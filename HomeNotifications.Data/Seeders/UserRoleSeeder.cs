namespace HomeNotifications.Data.Seeders;

using HomeNotifications.Data.Models;

using GeneralConstants = Common.GeneralConstants;

internal class UserRoleSeeder
{
    internal UserRole[] GenerateRoles()
    {
        ICollection<UserRole> roles = new HashSet<UserRole>();
        UserRole role;

        role = new UserRole()
        {
            Id = 1,
            Name = GeneralConstants.AdminRoleName,
            Created_By_Id = Guid.Parse(GeneralConstants.AdminId),
            Created_Date = DateTime.Now
        };
        roles.Add(role);

        role = new UserRole()
        {
            Id = 2,
            Name = GeneralConstants.UserRoleName,
            Created_By_Id = Guid.Parse(GeneralConstants.AdminId),
            Created_Date = DateTime.Now
        };
        roles.Add(role);

        return roles.ToArray();
    }
}
