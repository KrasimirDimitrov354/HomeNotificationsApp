namespace HomeNotifications.WebApi.Controllers;

using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

using HomeNotifications.Web.Models.User;
using HomeNotifications.Services.Data.Interfaces;

using ErrorMessages = Common.ErrorMessages;
using GeneralConstants = Common.GeneralConstants;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService userService;
    private readonly IHomeAuthenticationService authenticationService;

    public UserController(IUserService userService,
        IHomeAuthenticationService authenticationService)
    {
        this.userService = userService;
        this.authenticationService = authenticationService;
    }

    [HttpPost]
    [Authorize]
    public IActionResult TestTokenAuthorization()
    {
        return Ok("You are authorized!");
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ClaimsPrincipal> LoginWebUser(UserLoginModel user)
    {
        string userId = await userService.GetUserByUsernameAsync(user.Username);
        if (userId == string.Empty)
        {
            throw new ArgumentException(ErrorMessages.User.InvalidCredentials);
        }

        bool credentialsAreValid = await userService.ValidateCredentialsAsync(user.Username, user.Password);
        if (!credentialsAreValid)
        {
            throw new ArgumentException(ErrorMessages.User.InvalidCredentials);
        }

        try
        {
            IEnumerable<Claim> claims = await authenticationService.GetClaims(userId);
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(claimsIdentity);
        }
        catch (Exception)
        {
            throw new InvalidOperationException(ErrorMessages.Unexpected);
        }
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> LoginApiUser(UserLoginModel user)
    {
        string userId = await userService.GetUserByUsernameAsync(user.Username);
        if (userId == string.Empty)
        {
            return BadRequest();
        }

        bool credentialsAreValid = await userService.ValidateCredentialsAsync(user.Username, user.Password);
        if (!credentialsAreValid)
        {
            return Unauthorized();
        }

        try
        {
            IEnumerable<Claim> claims = await authenticationService.GetClaims(userId);
            string token = authenticationService.GetToken(claims, GeneralConstants.SecurityKey);

            return Ok(token);
        }
        catch (Exception)
        {
            return BadRequest(ErrorMessages.Unexpected);
        }
    }
}
