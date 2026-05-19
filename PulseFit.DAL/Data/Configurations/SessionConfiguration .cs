using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseFit.DAL.Entities;

namespace PulseFit.DAL.Data.Configurations;

internal class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {

        builder.ToTable(buildAction: Tb =>
        {
            Tb.HasCheckConstraint(name: "SessionCapacityCheck", sql: "Capacity Between 1 and 25");
            Tb.HasCheckConstraint(name: "SessionEndDateCheck", sql: "EndDate > StartDate");
        });
    }
}

