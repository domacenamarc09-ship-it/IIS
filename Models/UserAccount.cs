using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

[Index(nameof(InstructorId), IsUnique = true)]   
[Index(nameof(Email), IsUnique = true)]          
public class UserAccount                
{
    public int Id { get; set; }

    [Required, StringLength(15)]
    public string InstructorId { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;   // never the plain password
}
