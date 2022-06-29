using FishMarket.Dto;
using FishMarket.Dto.ServiceResponseDtos;

namespace FishMarket.Service.Abstract
{
    public interface IFishPriceService
    {
        Task<FishPriceUpdateApiResponseDto> UpdateFishPriceAsync(FishPriceUpdateDto fish);
        Task<int> DeleteFishPrice(Guid id);

    }
}
