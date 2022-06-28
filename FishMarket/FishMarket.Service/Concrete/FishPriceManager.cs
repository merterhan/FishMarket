using FishMarket.DataAccess.Abstract;
using FishMarket.Dto;
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
        public async Task<FishPrice> UpdateFishPriceAsync(FishPriceUpdateDto fishPriceUpdateDto)
        {
            var fishprice = _fishPriceDal.GetList(w => w.FishId == fishPriceUpdateDto.FishId).Result.OrderByDescending(o => o.CreatedOn).FirstOrDefault();
            await _fishPriceDal.Update(fishprice);
            return fishprice;
        }


    }
}
