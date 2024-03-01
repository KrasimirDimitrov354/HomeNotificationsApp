namespace HomeNotifications.Data.Models;

using System.ComponentModel.DataAnnotations;

using UserConstants = Common.EntityFieldValidation.User;

public class NotificationUser
{
    public NotificationUser()
    {
        Id = Guid.NewGuid();
        Notifications = new HashSet<Notification>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(UserConstants.UsernameMaxLength)]
    public string Username { get; set; } = null!;

    [Required]
    public string PasswordHash { get; set; } = null!;

    public bool ChangePassword {  get; set; }

    public Guid RoleId { get; set; }
    public virtual UserRoles Role { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; }

    //System columns
    public Guid Created_By_Id { get; set; }
    public DateTime Created_Date { get; set; }

    public Guid? Modified_By_Id { get; set; }
    public DateTime? Modified_Date { get; set; }
}
