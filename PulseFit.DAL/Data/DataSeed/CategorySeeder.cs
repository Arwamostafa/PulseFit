using Microsoft.EntityFrameworkCore;
using PulseFit.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PulseFit.DAL.Data.DataSeed
{
    public static class CategorySeeder
    {
        public static async Task SeedAsync(PluseFitDbContext pluseFitDbContext)
        {
            var ExsitCategories = await pluseFitDbContext.Categories.AnyAsync();
            if (ExsitCategories)
            {
                return;
            }
            var Categories = new List<Category>()
            {
                new Category() { Name = "Cardio"},
                new Category() { Name = "Strength" },
                new Category() { Name = "Flexibility" }
            };
            await pluseFitDbContext.Categories.AddRangeAsync(Categories);
            await pluseFitDbContext.SaveChangesAsync();
        }
    }
}
