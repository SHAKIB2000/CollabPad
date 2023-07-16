using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Reflection;
using CollabPad.Application;
using CollabPad.Infrastructure;
using CollabPad.Persistence;
using CollabPad.Persistence.Extensions;
using CollabPad.Web;
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hbc, lc) => lc
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration));

try
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    var migrationAssembly = Assembly.GetExecutingAssembly().FullName;

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
    {
        cb.RegisterModule(new ApplicationModule());
        cb.RegisterModule(new InfrastructureModule());
        cb.RegisterModule(new PersistenceModule(connectionString, migrationAssembly));
        cb.RegisterModule(new WebModule());
    });

    // Add services to the container.
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddIdentity();
    builder.Services.AddControllersWithViews();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapRazorPages();

    Log.Information("Application is starting...");

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application can not start.");
}
finally
{
    Log.CloseAndFlush();
}