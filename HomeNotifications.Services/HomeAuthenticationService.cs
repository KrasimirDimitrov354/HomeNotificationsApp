namespace HomeNotifications.Services.Data;

using System.Text;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using HomeNotifications.Data;
using HomeNotifications.Data.Models;
using HomeNotifications.Services.Data.Interfaces;

public class HomeAuthenticationService : IHomeAuthenticationService
{
    private readonly HomeNotificationsDbContext dbContext;

    public HomeAuthenticationService(HomeNotificationsDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Claim>> GetClaims(string userId)
    {
        NotificationUser user = await dbContext.Users
            .Include(u => u.Role)
            .FirstAsync(u => u.Id.ToString().ToLower() == userId.ToLower());

        return new HashSet<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Expired, user.ChangePassword.ToString()),
            new Claim(ClaimTypes.Role, user.Role.Name)
        };
    }

    public string GetToken(IEnumerable<Claim> claims, string keyString)
    {
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        JwtSecurityToken token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
