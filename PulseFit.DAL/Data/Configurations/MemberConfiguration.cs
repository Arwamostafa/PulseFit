using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseFit.DAL.Entities;

namespace PulseFit.DAL.Data.Configurations
{
    internal class MemberConfiguration : UserConfiguration<Member>, IEntityTypeConfiguration<Member>
    {
        public new void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(m => m.CreatedAt)
                   .HasColumnName("JoinDate")
                   .HasColumnType("GETDATE()");

            base.Configure(builder);

        }

    }
}
