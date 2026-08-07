using Microsoft.EntityFrameworkCore;
using PulseFit.DAL.Entities;

namespace PulseFit.DAL.Data.DataSeed;

public static class PlanSeeding
{
    public static async Task SeedAsync(PluseFitDbContext pluseFitDbContext)
    {
        var ExsitPlans = await pluseFitDbContext.Plans.AnyAsync();
        if (ExsitPlans)
        {
            return;
        }
        var Plans = new List<Plan>()
            {
                new Plan() { Name = "Basic", Description = "Basic Plan for you", Price = 300, DurationInDays = 30 ,IsActive=true },
                new Plan() { Name = "Standard", Description = "Standard Plan", Price = 700, DurationInDays = 90 , IsActive=true },
                new Plan() { Name = "Premium", Description = "Premium Plan", Price = 1000, DurationInDays = 180 , IsActive=true }
            };
        await pluseFitDbContext.Plans.AddRangeAsync(Plans);
        await pluseFitDbContext.SaveChangesAsync();

    }
}

