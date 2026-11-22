using System.ComponentModel.DataAnnotations;

public class RegisterViewModel         
{
    [Required(ErrorMessage = "Instructor ID is required.")]
    [RegularExpression(@"^[A-Za-z0-9-]{4,15}$",
        ErrorMessage = "Instructor ID must be 4 to 15 characters (letters, numbers, and hyphens only).")]
    [Display(Name = "Instructor ID")]
    public string InstructorId { get; set; } = string.Empty;

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, MinimumLength = 2,
        ErrorMessage = "First name must be between 2 and 50 characters.")]
    [RegularExpression(@"^[A-Za-z .'-]+$",
        ErrorMessage = "First name may only contain letters.")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, MinimumLength = 2,
        ErrorMessage = "Last name must be between 2 and 50 characters.")]
    [RegularExpression(@"^[A-Za-z .'-]+$",
        ErrorMessage = "Last name may only contain letters.")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email (example: juan@school.edu).")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(50, MinimumLength = 8,
        ErrorMessage = "Password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).+$",
        ErrorMessage = "Password must contain at least one letter and one number.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please confirm your password.")]
    [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
