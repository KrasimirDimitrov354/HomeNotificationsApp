namespace HomeNotifications.Services.Data;

using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using HomeNotifications.Data;
using HomeNotifications.Web.Models.Type;
using HomeNotifications.Services.Data.Interfaces;

public class NotificationTypeService : INotificationTypeService
{
    private readonly HomeNotificationsDbContext dbContext;

    public NotificationTypeService(HomeNotificationsDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ICollection<TypeDropdownSelect>> GetTypesForDropdownSelectAsync()
    {
        ICollection<TypeDropdownSelect> types = await dbContext.NotificationTypes
            .Select(nt => new TypeDropdownSelect
            {
                Id = nt.Id,
                Name = nt.Name,
                Color = nt.Color,
            })
            .ToListAsync();

        return types;
    }

    public async Task<bool> TypeExistsByIdAsync(int typeId)
    {
        return await dbContext.NotificationTypes.AnyAsync(nt => nt.Id == typeId);
    }
}
