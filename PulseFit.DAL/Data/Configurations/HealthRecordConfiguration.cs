using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseFit.DAL.Entities;

namespace PulseFit.DAL.Data.Configurations;

internal class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecored>
{
    public void Configure(EntityTypeBuilder<HealthRecored> builder)
    {

        builder.ToTable("Member").HasKey(x => x.Id);

        builder.HasOne<Member>()
                .WithOne(x => x.HealthRecored)
                .HasForeignKey<HealthRecored>(x => x.Id);
    }
}


