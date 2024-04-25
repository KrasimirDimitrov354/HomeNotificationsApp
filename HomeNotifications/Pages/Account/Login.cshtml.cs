namespace HomeNotifications.Web.Pages.Account;

using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;

using HomeNotifications.Web.Models.User;
using HomeNotifications.WebApi.Controllers;
using HomeNotifications.Web.Infrastructure.Extensions;

public class LoginModel : PageModel
{
    private readonly UserController userController;

    public LoginModel(UserController userController)
    {
        this.userController = userController;

        UserLoginModel = new UserLoginModel();
    }

    [BindProperty]
    public UserLoginModel UserLoginModel { get; set; }

    public IActionResult OnGet()
    {
        if (User.IsLoggedIn())
        {
            return RedirectToPage("/Index");
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (User.IsLoggedIn())
        {
            return RedirectToPage("/Index");
        }

        try
        {
            ClaimsPrincipal claimsPrincipal = await userController.LoginWebUser(UserLoginModel);

            await HttpContext.SignInAsync(claimsPrincipal);
        }
        catch (Exception)
        {
            return Page();
        }

        return RedirectToPage("/Index");
    }
}
