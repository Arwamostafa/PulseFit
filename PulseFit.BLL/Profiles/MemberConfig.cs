using Mapster;
using PulseFit.BLL.ModelViews;
using PulseFit.DAL.Entities;

namespace PulseFit.BLL.Profiles;

public class MemberConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateMemberViewModel, Member>()
            .Map(dest => dest.PhoneNumber, s => s.Phone)
            .Map(dest => dest.Address.BuildingNumber, s => s.BuildingNumber)
            .Map(d => d.Address.City, s => s.City)
            .Map(d => d.Address.Street, s => s.Street)
            .Map(d => d.HealthRecored, s => s.HealthRecord);


        config.NewConfig<Member, MemberModelView>()
            .Map(d => d.Phone, s => s.PhoneNumber)
            .Map(d => d.PlanName, s => s.MemberShips.Select(m => m.Plan.Name).FirstOrDefault())
            .Map(d => d.MembershipStartDate, s => s.MemberShips.Select(ms => ms.StartDate.ToString("yyyy-MM-dd")).FirstOrDefault())
            .Map(d => d.MembershipEndDate, s => s.MemberShips.Select(ms => ms.EndDate.ToString("yyyy-MM-dd")).FirstOrDefault())
            .Map(d => d.Address, s => $"{s.Address.BuildingNumber} - {s.Address.City} - {s.Address.Street}");

        config.NewConfig<Member, MemberToUpdateViewModel>()
            .Map(d => d.Phone, s => s.PhoneNumber)
            .Map(d => d.BuildingNumber, s => s.Address.BuildingNumber)
            .Map(d => d.City, s => s.Address.City)
            .Map(d => d.Street, s => s.Address.Street);

        config.NewConfig<MemberToUpdateViewModel, Member>()
            .Map(d => d.PhoneNumber, s => s.Phone)
            .Map(d => d.Address.BuildingNumber, s => s.BuildingNumber)
            .Map(d => d.Address.City, s => s.City)
            .Map(d => d.Address.Street, s => s.Street);

        config.NewConfig<HealthRecored, HealthRecordViewModel>();
    }
}

