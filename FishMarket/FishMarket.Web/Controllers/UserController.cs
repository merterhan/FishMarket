using FishMarket.Client;
using FishMarket.Dto;
using FishMarket.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FishMarket.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;


        public UserController(IServiceScopeFactory serviceScopeFactory)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                _logger = scope.ServiceProvider.GetRequiredService<ILogger<UserController>>();
                _userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            }
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var result = await _userService.Login(new Dto.UserLoginDto { Email = userLoginDto.Email, Password = userLoginDto.Password });
            return Json(result);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return RedirectToAction("index", "home");
            }
            var user = await _userService.Get(new Guid(userId));

            if (user == null)
            {
                ViewBag.ErrorMessage = $"The UserId {userId} is invalid";
                return View("NotFound");
            }
            _userService.ConfirmEmailAsync(user, token);
            return null;
        }
    }
}
