using AutoMapper;
using FishMarket.Dto;
using FishMarket.Entities.Concrete;
using FishMarket.Service.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMapper _mapper;


        public FishMarketController(IServiceScopeFactory serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                _logger = scope.ServiceProvider.GetRequiredService<ILogger<FishMarketController>>();
                _mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
                _fishManager = scope.ServiceProvider.GetRequiredService<IFishService>();                
                _fishPriceManager = scope.ServiceProvider.GetRequiredService<IFishPriceService>();
            }
        }

        /// <summary>
        /// Creates a new fish with its price
        /// </summary>
        /// <param name="fishInsertDto"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost, Route("Insert")]
        public async Task<IActionResult> Insert([FromBody] FishInsertDto fishInsertDto)
        {
            try
            {
                var result = await _fishManager.AddAsync(fishInsertDto);

                return Ok(new InsertFishResponseDto
                {
                    Id = result.Id,
                    Price = fishInsertDto.Price,
                    Type = result.Type
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Concat(ex.Message, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Updates the price of given fish
        /// </summary>
        /// <param name="fishUpdateDto"></param>
        /// <returns></returns>

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPatch, Route("UpdateFishPrice")]
        public async Task<IActionResult> UpdateFishPrice([FromBody] FishPriceUpdateDto fishUpdateDto)
        {
            var model = _mapper.Map<FishPrice>(fishUpdateDto);
            await _fishPriceManager.UpdateFishPriceAsync(model);
            return Ok();
        }

        /// <summary>
        /// Lists all fishes with their most-recent prices
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet, Route("ListFishes")]
        public async Task<List<FishDto>> ListFishes()
        {
            var fishes = await _fishManager.ListFishesAsync();
            return fishes;
        }

        /// <summary>
        /// Deletes the fish and it's all prices in table
        /// </summary>
        /// <param name="fishId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete, Route("DeleteFish/{fishId}")]
        public async Task<IActionResult> DeleteFish([FromRoute] Guid fishId)
        {
            var fish = _fishManager.GetByIdAsync(fishId);
            if (fish == null)
                return BadRequest("Fish Not Found");
            try
            {
                await _fishManager.Delete(fishId);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Concat(ex.Message, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
                return BadRequest(ex.Message);
            }

            return Ok(await fish);
        }
    }
}
