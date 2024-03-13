namespace HomeNotifications.Data.Seeders;

using HomeNotifications.Common;
using HomeNotifications.Data.Models;

using GeneralConstants = Common.GeneralConstants;

internal class NotificationUserSeeder
{
    internal NotificationUser[] GenerateUsers()
    {
        ICollection<NotificationUser> users = new HashSet<NotificationUser>();
        NotificationUser user;

        user = new NotificationUser()
        {
            Id = Guid.Parse(GeneralConstants.AdminId),
            Username = GeneralConstants.AdminUsername,
            PasswordHash = PasswordHasher.Hash(GeneralConstants.AdminPassword),
            ChangePassword = false,
            RoleId = 1,
            Created_By_Id = Guid.Parse(GeneralConstants.AdminId),
            Created_Date = DateTime.Now
        };
        users.Add(user);

        return users.ToArray();
    }
}
