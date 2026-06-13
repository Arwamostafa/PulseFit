using PulseFit.BLL;
using PulseFit.DAL;
using PulseFit.PL;

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
        builder.Services.AddDataAccessLayerConfigurations()
                        .AddBussnissLogicConfigurations()
                        .AddPresentationServices();

        //builder.Services.AddScoped<IMemberService, MemeberService>();


        var app = builder.Build();

        await app.SeedData();

        app.Middelwares();

        app.Run();
    }
}

