using Microsoft.AspNetCore.Identity;
using PulseFit.DAL;
using PulseFit.DAL.Data.DataSeed;
using PulseFit.DAL.Entities;

namespace PulseFit;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<PluseFitDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IPlanRepository, PlanRepository>();
        builder.Services.AddScoped<IMemberService, MemeberService>();


        var app = builder.Build();

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

        app.UseMiddleware<GlobalExceptionMiddleware>();

        if (!app.Environment.IsDevelopment())
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}

