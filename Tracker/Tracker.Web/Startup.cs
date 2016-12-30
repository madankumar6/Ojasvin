namespace Tracker.Web
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using MySQL.Data.EntityFrameworkCore.Extensions;
    using Services.Identity;
    using System;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Text;
    using System.Threading.Tasks;
    using Tracker.DAL;
    using Tracker.Entities.Identity;
    using Tracker.Web.Services;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            this.Configuration = builder.Build();

            secretKey = Configuration["TokenAuthentication:SecretKey"];
            signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
        }

        public IConfigurationRoot Configuration { get; }

        // secretKey contains a secret passphrase only your server knows
        public string secretKey = string.Empty;
        public SymmetricSecurityKey signingKey = null;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            ConfigureDatabase(services);
            //services.AddDbContext<UserDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:Tracker"], b => b.MigrationsAssembly("Tracker.DAL")), ServiceLifetime.Scoped);

            services.AddMvc(config =>
            {
                config.CacheProfiles.Add("TrackerCache", new CacheProfile()
                {
                    NoStore = true,
                    Location = ResponseCacheLocation.None
                });

                var authorizePolicy = new AuthorizationPolicyBuilder().AddRequirements(new TrackerRBACRequirement()).RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(authorizePolicy));
                //config.Filters.Add(new ResponseCacheFilter());
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("TrackerRBACPolicy", policy => policy.Requirements.Add(new TrackerRBACRequirement()));
            });

            services.AddIdentity<User, Role>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 3;
                config.Cookies.ApplicationCookie.LoginPath = "/Account/Login/";
            })
                .AddEntityFrameworkStores<UserDbContext, int>();

            // Add application services.
            services.AddTransient<IAuthorizationHandler, TrackerRBACHandler>();
            services.AddTransient<IAuthorizationHandler, TraclerRBACIsBannedHandler>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            ///app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();
            app.UseFileServer();

            app.UseStaticFiles(new StaticFileOptions());

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = Configuration["TokenAuthentication:Issuer"],

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = Configuration["TokenAuthentication:Audience"],

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            var tokenProviderOptions = new TokenProviderOptions
            {
                Path = Configuration["TokenAuthentication:TokenPath"],
                Audience = Configuration["TokenAuthentication:Audience"],
                Issuer = Configuration["TokenAuthentication:Issuer"],
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                IdentityResolver = GetIdentity
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                LoginPath = new PathString("/Account/Login/"),
                AccessDeniedPath = new PathString("/Account/Forbidden/"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                AuthenticationScheme = "Cookie",
                CookieName = "access_token",
                TicketDataFormat = new CookieJwtDataFormat(SecurityAlgorithms.HmacSha256, tokenValidationParameters)
            });

            //app.UseMiddleware<TokenProviderMiddleware>(Options.Create(tokenProviderOptions));

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //var filename = @"Data/DBSeeders/MenuSeeder.json";
            //var dataText = System.IO.File.ReadAllText(filename);
            //DBSeeder.SeedDB(dataText, app.ApplicationServices);
        }

        public void ConfigureDatabase(IServiceCollection services)
        {
            string database = Configuration["Data:Database"];
            string connection = Configuration["Data:Connection"];
            string connectionString = $"ConnectionStrings:{connection}";

            switch (database)
            {
                case "SqlServer":
                    services.AddDbContext<UserDbContext>(options => options.UseSqlServer(Configuration[connectionString], b => b.MigrationsAssembly("Tracker.DAL")));
                    break;
                case "MySql":
                    services.AddDbContext<UserDbContext>(options => options.UseMySQL(Configuration[connectionString], b => b.MigrationsAssembly("Tracker.DAL")));
                    break;
                default:
                    services.AddDbContext<UserDbContext>(options => options.UseSqlServer(Configuration[connectionString], b => b.MigrationsAssembly("Tracker.DAL")));
                    break;
            }
        }

        private Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            if (username == "TEST" && password == "TEST123")
            {
                return Task.FromResult(new ClaimsIdentity(new System.Security.Principal.GenericIdentity(username, "Token"), new Claim[] { }));
            }
            //var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
            //if (result.Succeeded)
            //{
            //    var user = await _userManager.FindByEmailAsync(username);
            //    var claims = await _userManager.GetClaimsAsync(user);

            //    return new ClaimsIdentity(new GenericIdentity(username, "Token"), claims);
            //}

            // Credentials are invalid, or account doesn't exist
            return Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
