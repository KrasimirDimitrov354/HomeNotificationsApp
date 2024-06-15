namespace HomeNotifications.Web.Infrastructure.Extensions;

using System.Security.Claims;

using GeneralConstants = Common.GeneralConstants;

public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// This method checks whether the user is logged in or not, and returns a boolean value.
    /// </summary>
    public static bool IsLoggedIn(this ClaimsPrincipal user)
    {
        return user.Identity != null && user.Identity.IsAuthenticated;
    }

    /// <summary>
    /// This method checks whether the logged in user is an administrator, and returns a boolean value.
    /// </summary>
    public static bool IsAdmin(this ClaimsPrincipal user)
    {
        return user.IsInRole(GeneralConstants.AdminRoleName);
    }

    /// <summary>
    /// This method returns the ID of the user taken from the user's claims, or a default value.
    /// </summary>
    public static string? GetId(this ClaimsPrincipal user)
    {
        return user.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
    }
}
