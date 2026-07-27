using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseFit.DAL.Entities;

namespace PulseFit.DAL.Data.Configurations;

internal class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.Property(propertyExpression: X => X.Name)
                .HasColumnType(typeName: "varchar")
                .HasMaxLength(maxLength: 50);

        builder.Property(propertyExpression: X => X.Description)
                .HasColumnType(typeName: "varchar")
                .HasMaxLength(maxLength: 100);

        builder.Property(x => x.Price)
                .HasPrecision(10, 2);

        builder.ToTable(tb =>
        {
            tb.HasCheckConstraint("PlanDurationCheck", "DurationInDays Between 1 and 365");
        });

        builder.HasIndex(X => X.Name)
                .IsUnique();

    }
}

