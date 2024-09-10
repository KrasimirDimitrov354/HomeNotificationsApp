namespace HomeNotifications.Services.Data;

using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

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

    public async Task<ICollection<NotificationPreviewModel>> GetLatestNotificationsAsync()
    {
        ICollection<NotificationPreviewModel> latestNotifications = await dbContext.Notifications
            .OrderBy(n => n.Created_Date)
            .Take(5)
            .Include(n => n.Owner)
            .Include(n => n.Type)
            .Select(n => new NotificationPreviewModel
            {
                Id = n.Id,
                Owner = n.Owner.Username,
                Priority = n.Type.Name,
                Color = n.Type.Color
            })
            .ToListAsync();

        return latestNotifications;
    }
}
