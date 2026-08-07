using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseFit.DAL.Entities;

namespace PulseFit.DAL.Data.Configurations;

internal class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.ToTable(buildAction: Tb =>
        {
            Tb.HasCheckConstraint(name: "SessionCapacityCheck", sql: "Capacity Between 1 and 25");
            Tb.HasCheckConstraint(name: "SessionEndDateCheck", sql: "EndDate > StartDate");
        });

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Sessions)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);


        // when we want to filter out the deleted entities from the query results, we can use the HasQueryFilter method to define a global query filter for the entity. This will automatically apply the filter to all queries for the entity, so we don't have to remember to add it manually each time.
        builder.HasQueryFilter(x => !x.IsDeleted);

    }
}

