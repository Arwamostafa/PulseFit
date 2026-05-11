using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseFit.DAL.Entities;

namespace PulseFit.DAL.Data.Configurations
{
    internal class TrainerConfiguration : UserConfiguration<Trainer>, IEntityTypeConfiguration<Trainer>
    {
        public new void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(m => m.CreatedAt)
                  .HasColumnName("HireDate")
                  .HasColumnType("GETDATE()");

            base.Configure(builder);
        }
    }
}
