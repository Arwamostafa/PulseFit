using Microsoft.AspNetCore.Identity;
using PulseFit.DAL;
using PulseFit.DAL.Data.DataSeed;
using PulseFit.DAL.Entities;
using Serilog;

namespace PulseFit.PL;

public static class Configurations
{
    public async static Task SeedData(this WebApplication app)
    {

        using var scope = app.Services.CreateScope();
        var dbContextObj = scope.ServiceProvider.GetRequiredService<PluseFitDbContext>();
        var roleMnagerObj = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManagerObj = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

        var pendingMigrations = dbContextObj.Database.GetPendingMigrations();
        if (pendingMigrations.Any())
        {
            dbContextObj.Database.Migrate();
        }
        await IdentityDataSeeding.SeedAsync(userManagerObj, roleMnagerObj);
        await PlanSeeding.SeedAsync(dbContextObj);
    }
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, IdentityRole>()
                 .AddEntityFrameworkStores<PluseFitDbContext>()
                 .AddDefaultTokenProviders();
        return services;
    }

    public static IApplicationBuilder Middelwares(this WebApplication app)
    {

        app.UseMiddleware<GlobalExceptionMiddleware>();

        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();

        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseSerilogRequestLogging();


        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        return app;
    }

}

