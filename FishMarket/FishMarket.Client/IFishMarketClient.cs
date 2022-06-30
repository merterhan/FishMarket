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
        [Patch("/FishMarket/UpdateFishPrice/{FishId}/{Price}")]
        Task<IActionResult> UpdateFishPrice([Body] Guid fishId, int price);

        [Get("/FishMarket/ListFishes")]
        Task<List<FishDto>> ListFishes();
    }
}