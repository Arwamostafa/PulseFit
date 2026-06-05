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
            modelBuilder.Entity<AppUser>(buildAction: Eb =>
            {

                Eb.Property(propertyExpression: X => X.FirstName)
                .HasColumnType(typeName: "varchar")
                .HasMaxLength(maxLength: 50);

                Eb.Property(propertyExpression: X => X.LastName)
                .HasColumnType(typeName: "varchar")
                .HasMaxLength(maxLength: 50);
            });
        }
        public DbSet<Member> Members { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MemberSession> MemberSessions { get; set; }
        public DbSet<MemberShip> MemberShips { get; set; }
        public DbSet<HealthRecored> HealthRecoreds { get; set; }
        public DbSet<Plan> Plans { get; set; }



    }

}
