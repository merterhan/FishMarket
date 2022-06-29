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
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> List()
        {
            var result = await _fishMarketClient.ListFishes();
            return View(result);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}