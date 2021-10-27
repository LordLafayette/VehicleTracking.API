using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VehicleTracking.Application.Interfaces;
using VehicleTracking.Application.Services;
using VehicleTracking.Infrastructure.Data;
using VehicleTracking.Infrastructure.Repositories;
using VehicleTracking.Infrastructure.UnitOfWork;

namespace VehicleTracking.IdentityServer
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

            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddDbContext<VehicleTrackingContext>(builder =>
            {
                builder.UseSqlServer(Configuration.GetConnectionString("VehicleTracking"), o => o.MigrationsAssembly("VehicleTracking.Infrastructure"));
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<VehicleTrackingContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer(o =>
            {
                o.UserInteraction.LoginUrl = "/auth/login";
                o.UserInteraction.LogoutUrl = "/auth/logout";
            })
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<IdentityUser>()
                .AddInMemoryClients(IdentityServerConfig.Clients)
                .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
                .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources);

            services.AddCors(o =>
            {
                o.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });

            services.AddAutoMapper(typeof(IApplicationService).Assembly);
            services.AddScoped<IAccountService, AccountService>();
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
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseCors();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
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
