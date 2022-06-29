using FishMarket.Dto;
using FishMarket.Service.Abstract;
using FishMarket.Web.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FishMarket.Web.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenManager;
    private readonly ISessionService _sessionService;


    public UserController(IServiceScopeFactory serviceScopeFactory)
    {
        using (var scope = serviceScopeFactory.CreateScope())
        {
            _logger = scope.ServiceProvider.GetRequiredService<ILogger<UserController>>();
            _userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            _tokenManager = scope.ServiceProvider.GetRequiredService<ITokenService>();
            _sessionService = scope.ServiceProvider.GetRequiredService<ISessionService>();
        }
    }
    public IActionResult Login()
    {
        if (_sessionService.GetUser() != null)
        {
            return RedirectToAction("Edit","FishMarket");
        }
        return View();
    }

    public IActionResult Logout()
    {
        _sessionService.ClearSession();
        return RedirectToAction("Login","User");
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        var result = await _userService.Login(new UserLoginDto { Email = userLoginDto.Email, Password = userLoginDto.Password });
        if (result.IsSuccess)
        {
            _sessionService.SetUser(result);
        }
        return RedirectToAction("Edit", "FishMarket");
        //return Json(result);
    }

    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(string email, string token)
    {
        _tokenManager.ValidateToken(token, email);
        var result = await _userService.ConfirmUserEmail(email);
        return Json(result);

    }
}
