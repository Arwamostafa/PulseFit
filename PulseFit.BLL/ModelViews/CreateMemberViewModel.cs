using System.ComponentModel.DataAnnotations;

namespace PulseFit.BLL.ModelViews;

public class CreateMemberViewModel
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
    [RegularExpression(@"^[a-zA-Z\s]+$")]
    public string Name { get; set; } = default!;


    [Required(ErrorMessage = "email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Email must be between 5 and 100 characters")]
    // datatype attribute is used to specify the type of data for the property, in this case, it indicates that the Email property should be treated as an email address. This can help with validation and formatting when working with the property in various contexts, such as forms or APIs.
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Phone number is required")]
    [RegularExpression(@"^01[0152][0-9]{8}$", ErrorMessage = "Phone number must be a valid Egyptian phone number")]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; } = default!;


    [Required(ErrorMessage = "Date Of Birth Is Required")]
    [DataType(dataType: DataType.Date)]
    public DateOnly DateOfBirth { get; set; }


    [Required(ErrorMessage = "Gender Is Required")]
    public string Gender { get; set; }


    [Required(ErrorMessage = "Building Number Is Required")]
    [Range(minimum: 1, maximum: 9000, ErrorMessage = "Building Number Must Be Between 1 and 9000")]
    public int BuildingNumber { get; set; }


    [Required(ErrorMessage = "Street Is Required")]
    [StringLength(maximumLength: 30, MinimumLength = 2, ErrorMessage = "Street Must Be Between 2 and 30 Chars")]
    public string Street { get; set; } = null!;


    [Required(ErrorMessage = "City Is Required")]
    [StringLength(maximumLength: 30, MinimumLength = 2, ErrorMessage = "City Must Be Between 2 and 30 Chars")]
    [RegularExpression(pattern: @"^[a-zA-Z\s]+$", ErrorMessage = "Name Can Contain Only Letters And Spaces")]
    public string City { get; set; } = null!;

    [Required(ErrorMessage = "Health Record Is Required")]
    public HealthRecordViewModel HealthRecord { get; set; } = default!;

}

