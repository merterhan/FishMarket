using FishMarket.DataAccess.Abstract;
using FishMarket.Dto;
using FishMarket.Dto.ServiceResponseDtos;
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

        public async Task<FishPriceUpdateApiResponseDto> UpdateFishPriceAsync(FishPriceUpdateDto fishPriceUpdateDto)
        {
            var fishprice = _fishPriceDal.GetList(w => w.FishId == fishPriceUpdateDto.FishId).Result.OrderByDescending(o => o.CreatedOn).FirstOrDefault();
            var result = await _fishPriceDal.Update(fishprice);

            return new FishPriceUpdateApiResponseDto
            {
                Id = fishprice.Id,
                Price = fishprice.Price,
                Type = fishprice.Fish.Type,
                ChangedOn = fishprice.ChangedOn,
                Message = "Güncelleme Başarılı",
                IsSuccess = true
            };
        }


    }
}
