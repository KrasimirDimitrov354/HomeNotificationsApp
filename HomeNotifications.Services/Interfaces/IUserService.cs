namespace HomeNotifications.Services.Data.Interfaces;

using HomeNotifications.Web.Models.User;

public interface IUserService
{
    Task CreateUserAsync(UserCreateModel userModel, string adminId);

    Task<string> GetUserByUsernameAsync(string username);

    Task<bool> ValidateCredentialsAsync(string username, string password);
}
