using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Core.Data;
using Microsoft.EntityFrameworkCore;
using Core.Data.Repositories;
using Newtonsoft.Json.Serialization;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Core.Web.Lib;
using Core.Business;
using Core.Data.Infrastructure;

namespace Core.Web
{
    public class Startup
    {
        bool useInMemoryProvider = false;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string sqlConnectionString = Configuration.GetConnectionString("DefaultConnection");

            //try
            //{
            //    useInMemoryProvider = bool.Parse(Configuration["AppSettings:InMemoryProvider"]);
            //}
            //catch { }

            //services.AddDbContext<DecanterContext>(options => {
            //    switch (useInMemoryProvider)
            //    {
            //        case true:
            //            options.UseInMemoryDatabase();
            //            break;
            //        default:
            //            options.UseSqlServer(sqlConnectionString,
            //        b => b.MigrationsAssembly("Core.Web"));
            //            break;
            //    }
            //});

            services.AddDbContext<DecanterContext>(options => {
                options.UseSqlServer(sqlConnectionString); });

            // Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IServiceRepository, ServiceRepository>();
            //services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<UnitOfWorkService, UnitOfWorkService>();

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            // Enable Cors
            services.AddCors();

            // Add MVC services to the services container.
            services.AddMvc()
                .AddJsonOptions(opts =>
                {
                    // Force Camel Case to JSON
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();
            loggerFactory.AddDebug((x, y) => x == "Microsoft.EntityFrameworkCore.Storage.Internal.RelationalCommandBuilderFactory");

            app.UseMiddleware<CoreHttpApplication>();

            app.UseStaticFiles();
            // Add MVC to the request pipeline.
            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseExceptionHandler(
              builder =>
              {
                  builder.Run(
                    async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                        }
                    });
              });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                //routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });

            DecanterSeedData.Initialize(app.ApplicationServices);
        }
    }
}
