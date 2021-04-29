using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using VehicleInspection.API.Utilities;
using VehicleInspection.API.Services;
using VehicleInspection.API.Infrastructure;

namespace src
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Setup Database
            services.AddDbContext<VehicleInspectionContext>(options =>
               options.UseSqlite("Datasource=VehicleInspection.db"));
            // options.UseSqlServer(Configuration.GetConnectionString("VISQLServerFileConnection")));
            //   options.UseSqlite($"Filename=/Documents/Projects/VehicleInspection/vi-api/src/API/Infrastructure/VehicleInspection.db"));
            //  options.UseSqlite(Configuration.GetConnectionString("VIConnection")));
            //Configuration.GetConnectionString("VIConnection")

            // Vehicle Service
            services.AddTransient<IInspectionService, InspectionService>();
            services.AddTransient<IVehicleService, VehicleService>();

            // Versioning
            services.AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                });

            // Api Controllers & Documentation
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "src", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "src v1"));
            }

            //  app.UseHttpsRedirection();

            app.UseRouting();

            //  app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
