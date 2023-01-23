using System.ComponentModel.DataAnnotations;

namespace App02.Auth;

public class LoginModel
{
    
    [Required(ErrorMessage = "User Name is required")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}