using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PulseFit.DAL.Entities;

namespace PulseFit.DAL.Data.DataSeed;


public class IdentityDataSeeding
{
    public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var ExsitUsers = await userManager.Users.AnyAsync();
        var ExsitRoles = await roleManager.Roles.AnyAsync();

        if (!ExsitRoles)
        {

            var Roles = new List<IdentityRole>() {

            new() { Name = "SuperAdmin" },
            new() { Name = "Admin" }
            };
            foreach (var role in Roles)
            {
                await roleManager.CreateAsync(role);
            }

        }

        if (!ExsitUsers)
        {
            var MainAdmin = new AppUser()
            {
                FirstName = "Mohamed",
                LastName = "Omar",
                UserName = "MohamedOmar",
                Email = "MohamedOmar@gmail.com",
                PhoneNumber = "01123569654"
            };

            await userManager.CreateAsync(user: MainAdmin, password: "P@ssw0rd");
            await userManager.AddToRoleAsync(user: MainAdmin, role: "SuperAdmin");

            var Admin = new AppUser()
            {
                FirstName = "Aliaa",
                LastName = "Tarek",
                UserName = "AliaaTarek",
                Email = "AliaaTarek@gmail.com",
                PhoneNumber = "01123569856"

            };

            await userManager.CreateAsync(user: Admin, password: "P@ssw0rd");
            await userManager.AddToRoleAsync(user: Admin, role: "SuperAdmin");
        }
    }
}

