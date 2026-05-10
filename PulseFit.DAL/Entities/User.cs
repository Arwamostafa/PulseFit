
using Microsoft.EntityFrameworkCore;
using PulseFit.DAL.Entities.Enums;

namespace PulseFit.DAL.Entities;

public abstract class User : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public Gender Gender { get; set; }
    public Address Address { get; set; } = null!;

}

[Owned]
public class Address
{
    public int BuildingNumber { get; set; }
    public string street { get; set; } = null!;
    public string City { get; set; } = null!;

}

