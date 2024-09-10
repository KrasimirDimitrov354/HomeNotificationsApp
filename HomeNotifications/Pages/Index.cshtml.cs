namespace HomeNotifications.Web.Pages;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using HomeNotifications.Services.Data.Interfaces;
using HomeNotifications.WebApi.Controllers;
using HomeNotifications.Web.Models.Notification;
using HomeNotifications.Web.Infrastructure.Extensions;

[Authorize]
public class IndexModel : PageModel
{
    private readonly INotificationTypeService notificationTypeService;
    private readonly NotificationController notificationController;
    private readonly UserController userController;

    public IndexModel(INotificationTypeService notificationTypeService,
        NotificationController notificationController,
        UserController userController)
    {
        this.notificationTypeService = notificationTypeService;
        this.notificationController = notificationController;
        this.userController = userController;

        NotificationFormModel = new NotificationFormModel();
    }

    [BindProperty]
    public NotificationFormModel NotificationFormModel { get; set; }

    public void OnGet(string tClass, string tContent)
    {
        ViewData["toast-class"] = tClass;
        ViewData["toast-content"] = tContent;
    }

    public async Task<PartialViewResult> OnGetFormPartial()
    {
        NotificationFormModel.Types = await notificationTypeService.GetTypesForDropdownSelectAsync();

        return Partial("~/Pages/Partial/_FormPartial.cshtml", NotificationFormModel);
    }

    public async Task OnPostFormPartial()
    {
        string userId = User.GetId()!;
        await notificationController.CreateNotification(NotificationFormModel, userId);
    }

    public async Task<IActionResult> OnGetToken()
    {
        string userId = User.GetId()!;

        return await userController.GetTokenForLocalStorage(userId);
    }
}
