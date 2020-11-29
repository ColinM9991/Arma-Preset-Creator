using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ArmaPresetCreator.Web.Models;
using ArmaPresetCreator.Web.Services;
using AutoMapper;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Hosting;

namespace ArmaPresetCreator.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(setup =>
            {
                setup.InputFormatters.RemoveType<NewtonsoftJsonPatchInputFormatter>();
            }).AddNewtonsoftJson();

            var steamConfigurationSection = Configuration.GetSection("Steam");
            var steamApiUrl = steamConfigurationSection.Get<SteamOptions>()?.ApiUrl;
            
            services.Configure<SteamOptions>(steamConfigurationSection);
            services.AddScoped<RequestLogMiddleware>();
            services.AddScoped<ISteamApiRepository, SteamApiRepository>();
            services.AddScoped<ISteamApiService, SteamApiService>();
            services.AddScoped<IArmaPresetRequestCreator, ArmaPresetRequestCreator>();
            services.AddScoped<IMapper, Mapper>(factory => new Mapper(AutoMapperConfigurationCreator.CreateMappingConfiguration()));
            services.AddHttpClient<SteamApiService>(client => client.BaseAddress = new Uri(steamApiUrl));

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<RequestLogMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder =>
            {
                // Add CORS domain here
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(routes =>
            {
                routes.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
