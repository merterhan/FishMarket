using FishMarket.Dto;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace FishMarket.Client
{
    public interface IFishMarketClient
    {
        [Headers("Authorization: Bearer")]
        [Post("/FishMarket/Insert")]
        Task<IActionResult> Insert([Body] FishInsertDto fishInsertDto);

        [Headers("Authorization: Bearer")]
        [Delete("/FishMarket/DeleteFish/{FishId}")]
        Task<IActionResult> DeleteFish(Guid fishId);

        [Headers("Authorization: Bearer")]
        [Patch("/FishMarket/UpdateFishPrice")]
        Task<IActionResult> UpdateFishPrice([Body] FishPriceUpdateDto fishUpdateDto);

        [Get("/FishMarket/ListFishes")]
        Task<List<FishDto>> ListFishes();
    }
}