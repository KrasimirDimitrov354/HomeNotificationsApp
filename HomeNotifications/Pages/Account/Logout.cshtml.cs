namespace HomeNotifications.Web.Pages.Account;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;

public class LogoutModel : PageModel
{
    public async Task<IActionResult> OnPost(string returnUrl)
    {
        await HttpContext.SignOutAsync();

        return LocalRedirect(returnUrl);
    }
}
