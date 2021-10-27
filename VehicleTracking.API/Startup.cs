using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracking.API.Filters;
using VehicleTracking.Application.Interfaces;
using VehicleTracking.Application.Services;
using VehicleTracking.Infrastructure.Data;
using VehicleTracking.Infrastructure.Repositories;
using VehicleTracking.Infrastructure.UnitOfWork;

namespace VehicleTracking.API
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

            services.AddControllers(o =>
            {
                o.Filters.Add<RequestInformationFilter>();
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VehicleTracking.API", Version = "v1" });
            });

            services.AddAuthentication("JWT")
                .AddJwtBearer("JWT", o =>
                {
                    o.Authority = "https://localhost:5001";
                    o.TokenValidationParameters.ValidateAudience = false;
                });

            services.AddCors(o =>
            {
                o.AddDefaultPolicy(b =>
                {
                    b.AllowAnyHeader();
                    b.AllowAnyMethod();
                    b.AllowAnyOrigin();
                });
            });

            services.AddDbContext<VehicleTrackingContext>(builder =>
            {
                builder.UseSqlServer(Configuration.GetConnectionString("VehicleTracking"), o => o.MigrationsAssembly("VehicleTracking.Infrastructure"));
            });

            services.AddAutoMapper(typeof(IApplicationService).Assembly);
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<IVehicleTrackingUnitOfWork, VehicleTrackingUnitOfWork>();
            services.AddScoped(typeof(IVehicleTrackingRepository<>), typeof(VehicleTrackingRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VehicleTracking.API v1"));
            }

            app.UseCors();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
