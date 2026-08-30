using Mapster;
using Mapster.Utils;
using PulseFit.BLL.ModelViews;
using PulseFit.BLL.ModelViews.Enums;
using PulseFit.DAL.QueryServices.Dtos;

namespace PulseFit.BLL.Profiles
{
    internal class SessionConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<SessionInsexQueryServiceDTO, SessionViewModel>()
                .Map(dest => dest.Status, src => DetermineStatus(src.StartDate, src.EndDate));
        }

        private static SessionStatus DetermineStatus(DateTime startDate, DateTime endDate)
        {
            var now = DateTime.Now;
            if (now < startDate)
                return SessionStatus.Upcoming;
            if (now >= startDate && now <= endDate)
                return SessionStatus.Ongoing;
            
            return SessionStatus.Completed;
        }
    }
}
