using Microsoft.Extensions.DependencyInjection;
using PulseFit.BLL.Services.Contracts;
using PulseFit.BLL.Services.Services;

namespace PulseFit.BLL;

public static class Configurations
{
    public static IServiceCollection AddBussnissLogicConfigurations(this IServiceCollection services)
    {
        services.AddScoped<IMemberService, MemeberService>();
        services.AddScoped<IPlanService, PlanService>();
        return services;
    }
}

