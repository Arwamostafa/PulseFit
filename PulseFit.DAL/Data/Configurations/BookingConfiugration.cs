using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseFit.DAL.Entities;

namespace PulseFit.DAL.Data.Configurations
{
    public class BookingConfiugration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Property(x => x.IsAttended)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasIndex(x => new { x.MemberId, x.SessionId }).IsUnique();

            builder.HasOne(x => x.Member)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(x =>
            {
                x.HasCheckConstraint("CK_Booking_Date", "[Date] >= CAST(GETDATE() AS DATE)");
            });

        }
    }
}
