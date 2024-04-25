namespace HomeNotifications.Services.Data.Interfaces;

using System.Security.Claims;

public interface IHomeAuthenticationService
{
    Task<IEnumerable<Claim>> GetClaims(string userId);

    string GetToken(IEnumerable<Claim> claims, string securityKey);
}
