namespace HomeNotifications.Web.Pages;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

[Authorize]
public class IndexModel : PageModel
{
    public void OnGet(string tClass, string tContent)
    {
        ViewData["toast-class"] = tClass;
        ViewData["toast-content"] = tContent;
    }
}
