using PulseFit.DAL.Entities;

namespace PulseFit.DAL.Repositories.Interfaces
{
    public interface IPlanRepository
    {
        public Task<IEnumerable<Plan>> GetAllAsync();
        public Task<Plan?> GetIdAsync(int id);
        public Task<int> Update(Plan plan);


    }
}
