
using Microsoft.EntityFrameworkCore;
using PulseFit.DAL.Entities;
using PulseFit.DAL.Repositories.Interfaces;

namespace PulseFit.DAL.Repositories.Classes;

public class PlanRepository(PluseFitDbContext pluseFitDbContext) : IPlanRepository
{

    public async Task<IEnumerable<Plan>> GetAllAsync()
    {
        var plans = await pluseFitDbContext.Plans.ToListAsync();
        return plans;
    }

    public async Task<Plan?> GetIdAsync(int id)
    {
        var plan = await pluseFitDbContext.Plans.FindAsync(id);
        return plan;
    }

    public async Task<int> Update(Plan plan)
    {
        pluseFitDbContext.Plans.Update(plan);
        return await pluseFitDbContext.SaveChangesAsync();
    }
}

