using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseFit.DAL.Entities;

namespace PulseFit.DAL.Data.Configurations;

public class MemberSessionConfiguration : IEntityTypeConfiguration<MemberSession>
{
    public void Configure(EntityTypeBuilder<MemberSession> builder)
    {

        builder.Ignore(x => x.Id);
        builder.HasKey(x => new { x.MemberId, x.SessionId });

        builder.Property(ms => ms.CreatedAt)
            .HasColumnName("BookingDate")
            .HasDefaultValueSql("GETDATE()");


        builder.HasOne(ms => ms.Member)
                .WithMany(m => m.MemberSessions)
                .HasForeignKey(ms => ms.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ms => ms.Session)
                .WithMany(s => s.MemberSessions)
                .HasForeignKey(ms => ms.SessionId);
    }
}

