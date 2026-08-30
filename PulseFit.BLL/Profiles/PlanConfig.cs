using Mapster;
using PulseFit.BLL.ModelViews;
using PulseFit.DAL.Entities;

namespace PulseFit.BLL.Profiles;

public class PlanConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Plan, PlanModelView>()
            .Map(dest => dest.DurationDays, src => src.DurationInDays);

        config.NewConfig<PlanModelView, Plan>()
            .Map(dest => dest.DurationInDays, src => src.DurationDays);

        config.NewConfig<Plan, PlanToUpdateViewModel>()
            .Map(dest => dest.PlanName, src => src.Name)
            .Map(dest => dest.DurationDays, src => src.DurationInDays);

        config.NewConfig<PlanToUpdateViewModel, Plan>()
            .Map(dest => dest.Name, src => src.PlanName)
            .Map(dest => dest.DurationInDays, src => src.DurationDays);
    }
}
