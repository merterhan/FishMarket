using FishMarket.Dto;
using FishMarket.Entities.Concrete;

namespace FishMarket.Service.Abstract
{
    public interface IFishPriceService
    {
        Task<FishPrice> UpdateFishPriceAsync(FishPriceUpdateDto fish);

    }
}
