using System.ComponentModel.DataAnnotations;

namespace CityInfo.MVC.DataTransferObjects.Auth;

public class TokenRequestModel
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    public string? ReturnUrl { get; set; } = default;
}
