using FishMarket.Dto;
using FishMarket.Dto.ServiceResponseDtos;
using FishMarket.Entities.Concrete;

namespace FishMarket.Service.Abstract
{
    public interface IFishPriceService
    {
        Task<FishPriceUpdateApiResponseDto> UpdateFishPriceAsync(FishPrice fish);
        Task<int> DeleteFishPrice(Guid id);

    }
}
