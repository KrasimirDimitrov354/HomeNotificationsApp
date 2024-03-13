namespace HomeNotifications.Data.Seeders;

using HomeNotifications.Data.Models;

using GeneralConstants = Common.GeneralConstants;

internal class NotificationTypeSeeder
{
    internal NotificationType[] GenerateTypes()
    {
        ICollection<NotificationType> types = new HashSet<NotificationType>();
        NotificationType type;

        type = new NotificationType()
        {
            Id = 1,
            Name = "High Priority",
            Color = "#FF0000",
            Created_By_Id = Guid.Parse(GeneralConstants.AdminId),
            Created_Date = DateTime.Now
        };
        types.Add(type);

        type = new NotificationType()
        {
            Id = 2,
            Name = "Medium Priority",
            Color = "#FFFF00",
            Created_By_Id = Guid.Parse(GeneralConstants.AdminId),
            Created_Date = DateTime.Now
        };
        types.Add(type);

        type = new NotificationType()
        {
            Id = 3,
            Name = "Low Priority",
            Color = "#0080FF",
            Created_By_Id = Guid.Parse(GeneralConstants.AdminId),
            Created_Date = DateTime.Now
        };
        types.Add(type);

        return types.ToArray();
    }
}
