using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseFit.DAL.Entities;

namespace PulseFit.DAL.Data.Configurations;

internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(x => x.FirstName)
            .HasColumnType("varchar")
            .HasMaxLength(50);

        builder.Property(x => x.LastName)
            .HasColumnType("varchar")
            .HasMaxLength(50);
    }
}
