namespace HomeNotifications.Web.Pages.Account;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

using HomeNotifications.Web.Models.User;
using HomeNotifications.WebApi.Controllers;
using HomeNotifications.Services.Data.Interfaces;
using HomeNotifications.Web.Infrastructure.Extensions;

using MessageConstants = Common.Messages;
using GeneralConstants = Common.GeneralConstants;

[Authorize]
public class CreateModel : PageModel
{
    private readonly IRoleService roleService;
    private readonly UserController userController;

    public CreateModel(IRoleService roleService,
        UserController userController)
    {
        this.roleService = roleService;
        this.userController = userController;

        UserCreateModel = new UserCreateModel();
    }

    [BindProperty]
    public UserCreateModel UserCreateModel { get; set; }

    public async Task<IActionResult> OnGet()
    {
        if (User.IsAdmin())
        {
            UserCreateModel.Roles = await roleService.GetRolesForDropdownSelectAsync();

            return Page();
        }
        else 
        {
            ViewData["toast-class"] = GeneralConstants.ToastNotificationWarningClass;
            ViewData["toast-content"] = MessageConstants.UnauthorizedAccess;
        }

        return RedirectToPage("/Index", new { tClass = ViewData["toast-class"], tContent = ViewData["toast-content"] });
    }

    public async Task<IActionResult> OnPost()
    {
        if (User.IsAdmin())
        {
            try
            {
                string adminId = User.GetId()!;
                await userController.CreateUser(UserCreateModel, adminId);

                ViewData["toast-class"] = GeneralConstants.ToastNotificationSuccessClass;
                ViewData["toast-content"] = MessageConstants.User.UserCreated;
            }
            catch (ArgumentException arg)
            {
                ViewData["toast-class"] = GeneralConstants.ToastNotificationErrorClass;
                ViewData["toast-content"] = arg.Message;

                UserCreateModel.Roles = await roleService.GetRolesForDropdownSelectAsync();
                return Page();
            }
            catch (Exception)
            {
                ViewData["toast-class"] = GeneralConstants.ToastNotificationErrorClass;
                ViewData["toast-content"] = MessageConstants.UnexpectedError;

                UserCreateModel.Roles = await roleService.GetRolesForDropdownSelectAsync();
                return Page();
            }
        }
        else
        {
            ViewData["toast-class"] = GeneralConstants.ToastNotificationWarningClass;
            ViewData["toast-content"] = MessageConstants.UnauthorizedAccess;
        }
        
        return RedirectToPage("/Index", new {tClass = ViewData["toast-class"], tContent = ViewData["toast-content"] } );
    }
}
