using AutoMapper;
using FishMarket.Dto;
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
        private readonly IFishPriceService _fishPriceManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;


        public FishMarketController(IServiceScopeFactory serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                _logger = scope.ServiceProvider.GetRequiredService<ILogger<FishMarketController>>();
                _fishManager = scope.ServiceProvider.GetRequiredService<IFishService>();
                _fishPriceManager = scope.ServiceProvider.GetRequiredService<IFishPriceService>();
                _userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                _mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            }
        }
        [HttpPost, Route("Insert")]
        public async Task<IActionResult> Insert([FromBody] FishInsertDto fishInsertDto)
        {
            var result = await _fishManager.AddAsync(fishInsertDto);
            
            return Ok(new InsertFishResponseDto
            {
                Id = result.Id,
                Price = fishInsertDto.Price,
                Type = result.Type
            });
        }

        [HttpPatch, Route("UpdateFishPrice/{FishId}/{Price}")]
        public async Task<IActionResult> UpdateFishPrice([FromRoute] FishPriceUpdateDto fishUpdateDto)
        {
            await _fishPriceManager.UpdateFishPriceAsync(fishUpdateDto);
            return Ok();
        }
    }
}
