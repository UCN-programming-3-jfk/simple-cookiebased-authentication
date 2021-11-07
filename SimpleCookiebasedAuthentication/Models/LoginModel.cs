using System.ComponentModel.DataAnnotations;

namespace SimpleCookiebasedAuthentication.Models
{
public class LoginModel
{
    [Required]
    [RegularExpression(@"[^\s]+", ErrorMessage = "No spaces in username")]
    public string UserName { get; set; }


    [Required]
    [RegularExpression(@"[^\s]+", ErrorMessage = "No spaces in password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
}