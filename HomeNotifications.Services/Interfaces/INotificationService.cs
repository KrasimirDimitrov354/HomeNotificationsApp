namespace HomeNotifications.Services.Data.Interfaces;

using HomeNotifications.Web.Models.Notification;

public interface INotificationService
{
    Task CreateNotificationAsync(NotificationFormModel notificationFormModel, string userId);
}
