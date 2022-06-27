using FishMarket.Dto;
using FishMarket.Entities.Concrete;
using FishMarket.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FishMarket.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FishMarketController : ControllerBase
    {
        private readonly ILogger<FishMarketController> _logger;
        private readonly IFishService _fishManager;
        private readonly IUserService _userService;
        

        public FishMarketController(IServiceScopeFactory serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                _logger = scope.ServiceProvider.GetRequiredService<ILogger<FishMarketController>>();
                _fishManager = scope.ServiceProvider.GetRequiredService<IFishService>();
                _userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                

            }
        }
        [HttpPost, Route("Insert")]
        public async Task<IActionResult> Insert([FromBody] UserLoginDto userLoginDto)
        {
            var fish = new Fish
            {
                Id = Guid.NewGuid(),
                Type = "Lüfer",
                CreatedBy = Guid.NewGuid(),
                CreatedOn = DateTime.Now

            };
            await _fishManager.Add(fish);
            return Ok(fish);
        }
    }
}
