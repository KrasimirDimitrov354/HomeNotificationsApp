namespace HomeNotifications.Web.Models.Notification;

public class NotificationPreviewModel
{
    public int Id { get; set; }

    public string Owner { get; set; } = null!;

    public string Priority { get; set; } = null!;

    public string Color { get; set; } = null!;
}
