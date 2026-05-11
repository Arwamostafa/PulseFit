using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseFit.DAL.Entities;

namespace PulseFit.DAL.Data.Configurations;

public class UserConfiguration<T> : IEntityTypeConfiguration<T> where T : User
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(x => x.Name)
            .HasColumnType("varchar")
            .HasMaxLength(50);

        builder.Property(x => x.Email)
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder.Property(x => x.PhoneNumber)
            .HasColumnType("varchar")
            .HasMaxLength(11);


        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CheckValidEmailConstraint", "Email Like '_0_%.%'");
            t.HasCheckConstraint("CheckValidPhoneConstraint", "PhoneNumber Like '01%' and PhoneNumber Not Like '%[^0-9]%' ");

        });

        builder.HasIndex(indexExpression: X => X.Email).IsUnique();
        builder.HasIndex(indexExpression: X => X.PhoneNumber).IsUnique();

        builder.OwnsOne(x => x.Address, AddressBuilder =>
        {
            AddressBuilder.Property(propertyExpression: X => X.Street)
                                    .HasColumnType(typeName: "varchar")
                                    .HasMaxLength(maxLength: 30);

            AddressBuilder.Property(propertyExpression: X => X.City)
                                    .HasColumnName(name: "City")
                                    .HasColumnType(typeName: "varchar")
                                    .HasMaxLength(maxLength: 30);


            AddressBuilder.Property(propertyExpression: X => X.BuildingNumber)
                                    .HasColumnName(name: "BuildingNumber");
        });

    }
}

