using Microsoft.Extensions.DependencyInjection;
using PulseFit.DAL.Repositories.Classes;
using PulseFit.DAL.Repositories.Interfaces;

namespace PulseFit.DAL;

public static class DataAccessConfigurations
{
    public static IServiceCollection AddDataAccessLayerConfigurations(this IServiceCollection services)
    {

        services.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        //services.AddScoped<IPlanRepository, PlanRepository>();
        //services.AddScoped<IMemberRepository, MemberRepository>();
        //services.AddScoped<IBookingRepository, BookingRepository>();


        return services;
    }
}

