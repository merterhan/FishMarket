using FishMarket.Dto;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace FishMarket.Client
{
    public interface IFishMarketClient
    {
        [Post("/FishMarket/Insert")]
        Task<IActionResult> Insert([Body] FishInsertDto fishInsertDto);

        [Delete("DeleteFishPrice/{FishId}/")]
        Task<IActionResult> DeleteFishPrice([AliasAs("FishId")] Guid fishId);

        [Patch("UpdateFishPrice/{FishId}/{Price}")]
        Task<IActionResult> UpdateFishPrice([Body] FishPriceUpdateDto fishUpdateDto);
    }
}