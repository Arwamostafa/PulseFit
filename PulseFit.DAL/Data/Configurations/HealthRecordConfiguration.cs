using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseFit.DAL.Entities;

namespace PulseFit.DAL.Data.Configurations;

internal class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecored>
{
    public void Configure(EntityTypeBuilder<HealthRecored> builder)
    {

        builder.ToTable("Members").HasKey(x => x.Id); //Not Needed by convintion it will be understood

        builder.HasOne<Member>()
                .WithOne(x => x.HealthRecored)
                .HasForeignKey<HealthRecored>(x => x.Id);

        builder.Ignore(x => x.CreatedAt);
        builder.Ignore(x => x.UpdatedAt);
    }
}


