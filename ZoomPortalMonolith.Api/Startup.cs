
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ZoomPortalMonolith.Api.Context;
using ZoomPortalMonolith.Api.Extensions;
using ZoomPortalMonolith.Infrastructure.EntityFramework.Context;
using ZoomPortalMonolith.SharedKernal.Infrastructure;

namespace ZoomPortalMonolith.Api
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
            services.AddControllers();

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Zoom Portal API", Version = "v1" });
            });


            #endregion


            var domainContextConnectionString = Configuration["DomainContext"];
            services.AddDbContext<DomainUnitOfWork>(options =>
                options.UseNpgsql(domainContextConnectionString));


            services.AddScoped<IDomainUnitOfWork>(provider => provider.GetService<DomainUnitOfWork>());


            // inject request context
            services.AddScoped<IRequestContext, WebRequestContext>();



    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.ConfigureCustomExceptionMiddleware();

            #region Swagger

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zoom Portal API V1");
            });


            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
