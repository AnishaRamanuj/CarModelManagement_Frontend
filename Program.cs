using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Core services
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddResponseCompression();

var app = builder.Build();
app.UseStaticFiles();
// Third-party services (if any)
// builder.Services.AddDbContext<MyDbContext>();

// Configure the host
Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseIIS();
        webBuilder.ConfigureAppConfiguration((hostingContext, config) => {
            var env = hostingContext.HostingEnvironment;
            var ConfigPath = Path.Combine(env.ContentRootPath, "Config");

            config.AddJsonFile(Path.Combine(ConfigPath, "appsettings.json"), optional: true, reloadOnChange: true);

            if (env.IsEnvironment("Development"))
            {
                config.AddJsonFile(Path.Combine(ConfigPath, $"appsettings.{env.EnvironmentName}.json"), optional: true, reloadOnChange: true);
            }

            config.AddEnvironmentVariables();
        });
        webBuilder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
        });
    });

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
