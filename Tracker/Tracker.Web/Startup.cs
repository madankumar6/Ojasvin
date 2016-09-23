using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tracker.Web.Data;
using Tracker.Web.Models;
using Tracker.Web.Services;
using Tracker.DAL;
using Tracker.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace Tracker.Web
{
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
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            ConfigureDatabase(services);

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
                .AddEntityFrameworkStores<UserContext, int>();

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

            app.UseStaticFiles(new StaticFileOptions()
            {
                //OnPrepareResponse = (context) =>
                //{
                //    // Disable caching for all static files.
                //    context.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
                //    context.Context.Response.Headers["Pragma"] = "no-cache";
                //    context.Context.Response.Headers["Expires"] = "-1";
                //}
            });

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void ConfigureDatabase(IServiceCollection services)
        {
            string database = Configuration["Data:Database"];
            string connection = Configuration["Data:Connection"];
            string connectionString = $"ConnectionStrings:{connection}";

            switch (database)
            {
                case "SQLServer":
                    services.AddDbContext<UserContext>(options => options.UseSqlServer(Configuration[connectionString], b => b.MigrationsAssembly("Tracker.DAL")), ServiceLifetime.Scoped);
                    break;
                case "MySql":
                    services.AddDbContext<UserContext>(options => options.UseMySQL(Configuration[connectionString], b => b.MigrationsAssembly("Tracker.DAL")), ServiceLifetime.Scoped);
                    break;
                default:
                    services.AddDbContext<UserContext>(options => options.UseSqlServer(Configuration[connectionString], b => b.MigrationsAssembly("Tracker.DAL")), ServiceLifetime.Scoped);
                    break;
            }
        }
    }
}
