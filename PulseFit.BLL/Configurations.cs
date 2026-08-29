using Mapster;
using Microsoft.Extensions.DependencyInjection;
using PulseFit.BLL.Services.Contracts;
using PulseFit.BLL.Services.Services;
using System.Reflection;

namespace PulseFit.BLL;

public static class Configurations
{
    public static IServiceCollection AddBussnissLogicConfigurations(this IServiceCollection services)
    {
        services.AddScoped<IMemberService, MemeberService>();
        services.AddScoped<IPlanService, PlanService>();
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        return services;
    }
}

