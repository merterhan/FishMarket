﻿using AutoMapper;
using FishMarket.Dto;
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPatch, Route("UpdateFishPrice/{FishId}/{Price}")]
        public async Task<IActionResult> UpdateFishPrice([FromRoute] FishPriceUpdateDto fishUpdateDto)
        {
            await _fishPriceManager.UpdateFishPriceAsync(fishUpdateDto);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet, Route("GetList")]
        public async Task<List<FishDto>> ListFishes()
        {
            var fishes = await _fishManager.ListFishesAsync();
            return fishes;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete, Route("DeleteFishPrice/{FishId}/")]
        public async Task<IActionResult> DeleteFishPrice([FromRoute] Guid fishId)
        {
            await _fishPriceManager.DeleteFishPrice(fishId);
            return Ok();
        }
    }
}
