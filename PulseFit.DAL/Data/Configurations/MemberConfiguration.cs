using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseFit.DAL.Entities;

namespace PulseFit.DAL.Data.Configurations;

internal class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.Property(m => m.CreatedAt)
               .HasColumnName("JoinDate")
               .HasDefaultValueSql("GETDATE()");

        builder.Property(m => m.Photo)
               .HasMaxLength(500);
    }
}
