using System.ComponentModel.DataAnnotations;

namespace CityInfo.MVC.DataTransferObjects.Auth;

public class RegisterModel
{
    [Required, StringLength(100)]
    public string? FirstName { get; set; }

    [Required, StringLength(100)]
    public string? LastName { get; set; }

    [Required, StringLength(50)]
    public string? Username { get; set; }

    [Required, StringLength(128)]
    [EmailAddress]
    public string? Email { get; set; }
    public DateTime DateOfBirth { get; set; }

    [Required, StringLength(265)]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}
