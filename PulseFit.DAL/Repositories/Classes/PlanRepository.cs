using PulseFit.DAL.Entities;
using PulseFit.DAL.Repositories.Interfaces;

namespace PulseFit.DAL.Repositories.Classes;

public class PlanRepository(PluseFitDbContext pluseFitDbContext) : GenaricRepository<Plan>(pluseFitDbContext), IPlanRepository
{

    //public async Task<IEnumerable<Plan>> GetAllAsync(CancellationToken cancellationToken)
    //{
    //    var plans = await pluseFitDbContext.Plans.ToListAsync(cancellationToken);
    //    return plans;
    //}

    //public async Task<Plan?> GetIdAsync(int id, CancellationToken cancellationToken)
    //{
    //    var plan = await pluseFitDbContext.Plans.FindAsync(id, cancellationToken);
    //    return plan;
    //}

    //public void Update(Plan plan) => pluseFitDbContext.Plans.Update(plan);

}

