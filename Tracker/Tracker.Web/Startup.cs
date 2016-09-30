namespace Tracker.Web
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using MySQL.Data.EntityFrameworkCore.Extensions;

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
        }

        public IConfigurationRoot Configuration { get; }

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

                var authorizeFilter = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(authorizeFilter));
                config.Filters.Add(new ResponseCacheFilter());
            });

            services.AddIdentity<User, Role>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 3;
                config.Cookies.ApplicationCookie.LoginPath = "/Account/Login/";
            })
                .AddEntityFrameworkStores<UserDbContext, int>();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

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
    }
}
