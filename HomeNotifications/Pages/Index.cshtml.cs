namespace HomeNotifications.Web.Pages;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using HomeNotifications.Web.Models.Notification;
using HomeNotifications.Services.Data.Interfaces;

[Authorize]
public class IndexModel : PageModel
{
    private readonly INotificationTypeService notificationTypeService;

    public IndexModel(INotificationTypeService notificationTypeService)
    {
        this.notificationTypeService = notificationTypeService;

        NotificationFormModel = new NotificationFormModel();
    }

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
}
