namespace HomeNotifications.Services.Data.Interfaces;

using HomeNotifications.Web.Models.Type;

public interface INotificationTypeService
{
    Task<bool> TypeExistsByIdAsync(int typeId);

    Task<ICollection<TypeDropdownSelect>> GetTypesForDropdownSelectAsync();
}
