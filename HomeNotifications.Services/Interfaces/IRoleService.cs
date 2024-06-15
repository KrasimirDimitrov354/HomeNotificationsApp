namespace HomeNotifications.Services.Data.Interfaces;

using HomeNotifications.Web.Models.Role;

public interface IRoleService
{
    Task<bool> RoleExistsByIdAsync(int roleId);

    Task<ICollection<RoleDropdownModel>> GetRolesForDropdownSelectAsync();
}
