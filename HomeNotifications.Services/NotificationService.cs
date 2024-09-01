namespace HomeNotifications.Services.Data;

using System.Threading.Tasks;

using HomeNotifications.Data;
using HomeNotifications.Data.Models;
using HomeNotifications.Web.Models.Notification;
using HomeNotifications.Services.Data.Interfaces;

public class NotificationService : INotificationService
{
    private readonly HomeNotificationsDbContext dbContext;

    public NotificationService(HomeNotificationsDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateNotificationAsync(NotificationFormModel notificationDetails, string userId)
    {
        Notification notification = new Notification()
        {
            Content = notificationDetails.Content,
            TypeId = notificationDetails.TypeId,
            OwnerId = Guid.Parse(userId),
            Created_By_Id = Guid.Parse(userId),
            Created_Date = DateTime.Now,
        };

        await dbContext.Notifications.AddAsync(notification);
        await dbContext.SaveChangesAsync();
    }
}
