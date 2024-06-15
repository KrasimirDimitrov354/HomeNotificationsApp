namespace HomeNotifications.Services.Data;

using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using HomeNotifications.Data;
using HomeNotifications.Web.Models.Role;
using HomeNotifications.Services.Data.Interfaces;

public class RoleService : IRoleService
{
    private readonly HomeNotificationsDbContext dbContext;

    public RoleService(HomeNotificationsDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ICollection<RoleDropdownModel>> GetRolesForDropdownSelectAsync()
    {
        ICollection<RoleDropdownModel> roles = await dbContext.UserRoles
            .Select(u => new RoleDropdownModel()
            {
                Id = u.Id,
                Name = u.Name,
            })
            .ToListAsync();

        return roles;
    }

    public async Task<bool> RoleExistsByIdAsync(int roleId)
    {
        return await dbContext.UserRoles.AnyAsync(u => u.Id == roleId);
    }
}
