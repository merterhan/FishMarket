using FishMarket.DataAccess.Abstract;
using FishMarket.Dto;
using FishMarket.Dto.ServiceResponseDtos;
using FishMarket.Entities.Concrete;
using FishMarket.Service.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace FishMarket.Service.Concrete
{
    public class FishPriceManager : IFishPriceService
    {
        private readonly IFishPriceDal _fishPriceDal;

        public FishPriceManager(IServiceScopeFactory serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                _fishPriceDal = scope.ServiceProvider.GetRequiredService<IFishPriceDal>();
            }
        }

        public async Task<int> DeleteFishPrice(Guid id)
        {
           return await _fishPriceDal.Delete(id);
        }

        public async Task<FishPriceUpdateApiResponseDto> UpdateFishPriceAsync(FishPrice fishPrice)
        {
            fishPrice.CreatedOn = DateTime.Now;
            var result = await _fishPriceDal.Add(fishPrice);

            return new FishPriceUpdateApiResponseDto
            {
                Id = fishPrice.Id,
                Price = fishPrice.Price,
                Message = "Güncelleme Başarılı",
                IsSuccess = true
            };
        }


    }
}
