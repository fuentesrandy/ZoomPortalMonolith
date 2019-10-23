using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZoomPortalMonolith.Infrastructure.EntityFramework.Context;
using ZoomPortalMonolith.SharedKernal.Infrastructure;
using ZoomPortalMonolith.WebApp.Context;
using Swashbuckle.AspNetCore.Swagger;
using ZoomPortalMonolith.WebApp.Extensions;

namespace ZoomPortalMonolith.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });


            #region Swagger

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "Zoom Portal API", Version = "v1" });
                });
            }

            #endregion  


            var domainContextConnectionString = Configuration.GetConnectionString("DomainContext");
            services.AddDbContext<DomainUnitOfWork>(options =>
                options.UseSqlServer(domainContextConnectionString));


            services.AddScoped<IDomainUnitOfWork>(provider => provider.GetService<DomainUnitOfWork>());


            // inject request context
            services.AddScoped<IRequestContext, WebRequestContext>();



            // Automatically perform database migration
            services.BuildServiceProvider().GetService<DomainUnitOfWork>().Database.Migrate();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.ConfigureCustomExceptionMiddleware();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //}

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            #region Swagger

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zoom Portal API V1");
                });
            }

            #endregion

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
