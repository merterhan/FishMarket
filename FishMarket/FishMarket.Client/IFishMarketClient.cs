using FishMarket.Dto;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace FishMarket.Client
{
    public interface IFishMarketClient
    {
        [Post("/FishMarket/Insert")]
        Task<IActionResult> Insert([Body] FishInsertDto fishInsertDto);

        [Delete("/FishMarket/DeleteFishPrice/{FishId}")]
        Task<IActionResult> DeleteFishPrice([AliasAs("FishId")] Guid fishId);

        [Patch("/FishMarket/UpdateFishPrice/{FishId}/{Price}")]
        Task<IActionResult> UpdateFishPrice([Body] Guid fishId, int price);

        [Get("/FishMarket/ListFishes")]
        Task<List<FishDto>> ListFishes();
    }
}