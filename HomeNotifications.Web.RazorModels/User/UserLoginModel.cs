namespace HomeNotifications.Web.Models.User;

using System.ComponentModel.DataAnnotations;

public class UserLoginModel
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
