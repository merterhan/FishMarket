using FishMarket.Client;
using FishMarket.Service.Abstract;
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
        public Task<IActionResult> Login()
        {
            var result = _userService.Login(new Dto.UserLoginDto { Email = "info@cagrierhan.com", Password = "12345" });
            return null;
        }
    }
}
