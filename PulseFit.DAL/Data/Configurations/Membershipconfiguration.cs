

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

        builder.HasKey(X => new { X.MemberId, X.PlanId });
        builder.Ignore(x => x.Id);

    }
}

