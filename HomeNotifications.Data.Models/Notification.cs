namespace HomeNotifications.Data.Models;

using System.ComponentModel.DataAnnotations;

using NotificationConstants = Common.EntityFieldValidation.Notification;

public class Notification
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(NotificationConstants.ContentMaxLength)]
    public string Content { get; set; } = null!;

    public int TypeId { get; set; }
    public virtual NotificationType NotificationType { get; set; } = null!;

    public Guid OwnerId { get; set; }
    public virtual NotificationUser Owner { get; set; } = null!;

    //System columns
    public Guid Created_By_Id { get; set; }
    public DateTime Created_Date { get; set; }

    public Guid? Modified_By_Id { get; set; }
    public DateTime? Modified_Date { get; set; }
}
