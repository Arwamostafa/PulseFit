using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PulseFit.DAL.Entities;
using System.Reflection;

namespace PulseFit.DAL
{
    public class PluseFitDbContext(DbContextOptions<PluseFitDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        //public DbSet<Member> Members { get; set; }
        //public DbSet<Trainer> Trainers { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MemberSession> MemberSessions { get; set; }
        public DbSet<MemberShip> MemberShips { get; set; }
        public DbSet<HealthRecored> HealthRecoreds { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Booking> Bookings { get; set; }

    }
}
