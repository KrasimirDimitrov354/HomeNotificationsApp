namespace HomeNotifications.WebApi.Controllers;

using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

using HomeNotifications.Web.Models.User;
using HomeNotifications.Services.Data.Interfaces;

using Messages = Common.Messages;
using GeneralConstants = Common.GeneralConstants;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService userService;
    private readonly IRoleService roleService;
    private readonly IHomeAuthenticationService authenticationService;

    public UserController(IUserService userService,
        IRoleService roleService,
        IHomeAuthenticationService authenticationService)
    {
        this.userService = userService;
        this.roleService = roleService;
        this.authenticationService = authenticationService;
    }

    [HttpPost]
    [Authorize]
    public IActionResult TestTokenAuthorization()
    {
        return Ok("You are authorized!");
    }

    [HttpPost]
    [Authorize]
    public async Task CreateUser(UserCreateModel userDetails, string adminId)
    {
        bool usernameExists = await userService.GetUserByUsernameAsync(userDetails.Username) != string.Empty;
        if (usernameExists)
        {
            throw new ArgumentException(Messages.User.UsernameAlreadyExistsError);
        }

        if (userDetails.Password != userDetails.ConfirmPassword)
        {
            throw new ArgumentException(Messages.User.PasswordMismatchError);
        }

        bool roleExists = await roleService.RoleExistsByIdAsync(userDetails.RoleId);
        if (!roleExists)
        {
            throw new ArgumentException(Messages.Role.InvalidIdError);
        }

        await userService.CreateUserAsync(userDetails, adminId);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ClaimsPrincipal> LoginWebUser(UserLoginModel user)
    {
        string userId = await userService.GetUserByUsernameAsync(user.Username);
        if (userId == string.Empty)
        {
            throw new ArgumentException(Messages.User.InvalidCredentialsError);
        }

        bool credentialsAreValid = await userService.ValidateCredentialsAsync(user.Username, user.Password);
        if (!credentialsAreValid)
        {
            throw new ArgumentException(Messages.User.InvalidCredentialsError);
        }

        try
        {
            IEnumerable<Claim> claims = await authenticationService.GetClaims(userId);
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(claimsIdentity);
        }
        catch (Exception)
        {
            throw new InvalidOperationException(Messages.UnexpectedError);
        }
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> LoginApiUser(UserLoginModel user)
    {
        string userId = await userService.GetUserByUsernameAsync(user.Username);
        if (userId == string.Empty)
        {
            return BadRequest(Messages.User.InvalidCredentialsError);
        }

        bool credentialsAreValid = await userService.ValidateCredentialsAsync(user.Username, user.Password);
        if (!credentialsAreValid)
        {
            return Unauthorized(Messages.User.InvalidCredentialsError);
        }

        try
        {
            IEnumerable<Claim> claims = await authenticationService.GetClaims(userId);
            string token = authenticationService.GetToken(claims, GeneralConstants.SecurityKey);

            return Ok(token);
        }
        catch (Exception)
        {
            return BadRequest(Messages.UnexpectedError);
        }
    }
}
