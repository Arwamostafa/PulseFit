namespace PulseFit.DAL.Data.DataSeed;

public static class DatabaseSeeder
{
    public static async Task SeedAllAsync(PluseFitDbContext context)
    {
        await PlanSeeding.SeedAsync(context);
        await CategorySeeder.SeedAsync(context);

    }
}

