namespace HomeNotifications.Web.Infrastructure.Extensions;

using System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// This method checks whether the user is logged in or not, and returns a boolean value.
    /// </summary>
    public static bool IsLoggedIn(this ClaimsPrincipal user)
    {
        return user.Identity != null && user.Identity.IsAuthenticated;
    }
}
