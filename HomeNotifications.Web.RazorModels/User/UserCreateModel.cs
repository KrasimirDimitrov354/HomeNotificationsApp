namespace HomeNotifications.Web.Models.User;

using System.ComponentModel.DataAnnotations;

using HomeNotifications.Web.Models.Role;

using UserConstants = Common.EntityFieldValidation.User;

public class UserCreateModel
{
    public UserCreateModel()
    {
        Roles = new HashSet<RoleDropdownModel>();
    }

    [Required]
    [StringLength(UserConstants.UsernameMaxLength, 
        MinimumLength = UserConstants.UsernameMinLength, 
        ErrorMessage = UserConstants.UsernameLengthErrorMessage)]
    public string Username { get; set; } = null!;

    [Required]
    [StringLength(UserConstants.PasswordMaxLength,
        MinimumLength = UserConstants.PasswordMinLength,
        ErrorMessage = UserConstants.PasswordLengthErrorMessage)]
    public string Password { get; set; } = null!;

    [Required]
    [StringLength(UserConstants.PasswordMaxLength,
        MinimumLength = UserConstants.PasswordMinLength,
        ErrorMessage = UserConstants.PasswordLengthErrorMessage)]
    [Display(Name = UserConstants.ConfirmPasswordFieldDisplayName)]
    public string ConfirmPassword { get; set; } = null!;

    [Display(Name = UserConstants.RoleFieldDisplayName)]
    public int RoleId { get; set; }

    public ICollection<RoleDropdownModel> Roles { get; set; }
}
