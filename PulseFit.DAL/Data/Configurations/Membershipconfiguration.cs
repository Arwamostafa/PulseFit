

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseFit.DAL.Entities;

namespace PulseFit.DAL.Data.Configurations;

public class Membershipconfiguration : IEntityTypeConfiguration<MemberShip>
{
    public void Configure(EntityTypeBuilder<MemberShip> builder)
    {
        builder.Property(x => x.CreatedAt)
            .HasColumnName("StartDate")
            .HasDefaultValueSql("GETDATE()");

        builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_DateRange", "EndDate > StartDate");
            });
        // develop it on futer to make user dont have more than active plan in the same time
        //builder.HasIndex(x => new { x.MemberId, x.PlanId });

        builder.HasKey(X => new { X.MemberId, X.PlanId });
        builder.Ignore(x => x.Id);

    }
}

