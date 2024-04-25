namespace HomeNotifications.Services.Data;

using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using HomeNotifications.Data;
using HomeNotifications.Data.Models;
using HomeNotifications.Common;
using HomeNotifications.Web.Models.User;
using HomeNotifications.Services.Data.Interfaces;

public class UserService : IUserService
{
    private readonly HomeNotificationsDbContext dbContext;

    public UserService(HomeNotificationsDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateUserAsync(UserCreateModel userModel, string adminId)
    {
        NotificationUser user = new NotificationUser()
        {
            Username = userModel.Username,
            PasswordHash = PasswordHasher.Hash(userModel.Password),
            ChangePassword = true,
            RoleId = userModel.RoleId,
            Created_By_Id = Guid.Parse(adminId),
            Created_Date = DateTime.Now,
        };

        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task<string> GetUserByUsernameAsync(string username)
    {
        NotificationUser? user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());

        if (user != null)
        {
            return user.Id.ToString();
        }

        return string.Empty;
    }

    public async Task<bool> ValidateCredentialsAsync(string username, string password)
    {
        NotificationUser user = await dbContext.Users
            .FirstAsync(u => u.Username.ToLower() == username.ToLower());

        return PasswordHasher.Verify(password, user.PasswordHash);
    }
}
