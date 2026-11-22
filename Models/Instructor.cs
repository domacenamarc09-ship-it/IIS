using System.ComponentModel.DataAnnotations;

public class Instructor
{
    public int Id { get; set; }                      

    [Required(ErrorMessage = "Full name is required.")]
    [StringLength(100, MinimumLength = 5,
        ErrorMessage = "Full name must be between 5 and 100 characters.")]
    [RegularExpression(@"^[A-Za-z .,'-]+$",
        ErrorMessage = "Full name may only contain letters, spaces, and . , ' - symbols.")]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Age is required.")]
    [Range(21, 70, ErrorMessage = "Age must be a whole number from 21 to 70.")]
    public int? Age { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(150, MinimumLength = 5,
        ErrorMessage = "Address must be between 5 and 150 characters.")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Designation is required (example: Instructor I).")]
    [StringLength(50, ErrorMessage = "Designation must not exceed 50 characters.")]
    public string Designation { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a gender.")]
    [RegularExpression("^(Male|Female)$", ErrorMessage = "Gender must be Male or Female.")]
    public string Gender { get; set; } = string.Empty;

    [Required(ErrorMessage = "Contact number is required.")]
    [RegularExpression(@"^(09|\+639)\d{9}$",
        ErrorMessage = "Enter a valid mobile number (example: 09171234567).")]
    [Display(Name = "Contact Number")]
    public string ContactNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Majorship is required.")]
    [StringLength(80, ErrorMessage = "Majorship must not exceed 80 characters.")]
    public string Majorship { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bachelor degree is required.")]
    [StringLength(100, ErrorMessage = "Bachelor degree must not exceed 100 characters.")]
    [Display(Name = "Bachelor Degree")]
    public string BachelorDegree { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-mail address is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid e-mail address (example: juan@school.edu).")]
    [Display(Name = "E-mail Address")]
    public string EmailAddress { get; set; } = string.Empty;
}
