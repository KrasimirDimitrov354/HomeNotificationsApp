namespace HomeNotifications.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using HomeNotifications.Web.Models.Notification;
using HomeNotifications.Services.Data.Interfaces;

using GeneralConstants = Common.GeneralConstants;
using TypeConstants = Common.EntityFieldValidation.NotificationType;
using NotificationConstants = Common.EntityFieldValidation.Notification;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class NotificationController : ControllerBase
{
    private readonly INotificationTypeService notificationTypeService;
    private readonly INotificationService notificationService;

    public NotificationController(INotificationTypeService notificationTypeService,
        INotificationService notificationService)
    {
        this.notificationTypeService = notificationTypeService;
        this.notificationService = notificationService;
    }

    [HttpPost]
    public async Task CreateNotificationAsync(NotificationFormModel notificationDetails, string userId)
    {
        try
        {
            bool notificationTypeExists = await notificationTypeService.TypeExistsByIdAsync(notificationDetails.TypeId);
            if (!notificationTypeExists)
            {
                throw new ArgumentException(TypeConstants.InvalidNotificationTypeId);
            }

            if (notificationDetails.Content.Length < NotificationConstants.ContentMinLength
                || notificationDetails.Content.Length > NotificationConstants.ContentMaxLength)
            {
                throw new ArgumentOutOfRangeException(NotificationConstants.NotificationLengthErrorMessage);
            }

            await notificationService.CreateNotificationAsync(notificationDetails, userId);
        }
        catch (Exception)
        {
            throw new ApplicationException(GeneralConstants.UnexpectedErrorMessage);
        }
    }
}
