using FishMarket.Client;
using FishMarket.Service.Abstract;
using FishMarket.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FishMarket.Web.Controllers
{
    public class FishMarketController : Controller
    {
        private readonly ILogger<FishMarketController> _logger;
        private readonly IFishMarketClient _fishMarketClient;
        private readonly IUserService _userService;


        public FishMarketController(IServiceScopeFactory serviceScopeFactory)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                _logger = scope.ServiceProvider.GetRequiredService<ILogger<FishMarketController>>();
                _fishMarketClient = scope.ServiceProvider.GetRequiredService<IFishMarketClient>();
                _userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            }
        }

        public async Task<IActionResult> Index()
        {
            var result = await _userService.Login(new Dto.UserLoginDto { Email = "info@cagrierhan.com", Password = "12345" });
            return View(result);
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
                ViewBag.ErrorMessage =$"The UserId {userId} is invalid";
                return View("NotFound");
            }
            _userService.ConfirmEmailAsync(user, token);
            return null;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}