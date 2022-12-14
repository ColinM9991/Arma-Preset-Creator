using ArmaPresetCreator.Web;
using Microsoft.AspNetCore.Builder;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration));

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app, app.Environment);

await app.RunAsync();