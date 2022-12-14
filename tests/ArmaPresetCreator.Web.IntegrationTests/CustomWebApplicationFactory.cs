using ArmaPresetCreator.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace ArmaPresetCreator.Web.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(servicesConfiguration =>
            {
                servicesConfiguration.AddScoped<IFileReader, FileReader>();
                servicesConfiguration.AddScoped<ISteamApiService, HardcodedSteamApiService>();
            });
        }
    }
}