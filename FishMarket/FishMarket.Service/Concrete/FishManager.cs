using FishMarket.DataAccess.Abstract;
using FishMarket.Dto;
using FishMarket.Entities.Concrete;
using FishMarket.Service.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace FishMarket.Service.Concrete
{
    public class FishManager : IFishService
    {

        private readonly IFishDal _fishDal;
        private readonly IFishPriceDal _fishPriceDal;

        public FishManager(IServiceScopeFactory serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                _fishDal = scope.ServiceProvider.GetRequiredService<IFishDal>();
                _fishPriceDal = scope.ServiceProvider.GetRequiredService<IFishPriceDal>();

            }
        }
        public async Task<Fish> AddAsync(FishInsertDto fishInsertDto)
        {

            var now = DateTime.Now;

            var fish = new Fish
            {
                Id = Guid.NewGuid(),
                Type = fishInsertDto.Name,
                CreatedOn = now
            };

            await _fishDal.Add(fish);

            var fishPrice = new FishPrice
            {
                FishId = fish.Id,
                Price = fishInsertDto.Price,
                CreatedOn = now
            };
            
            await _fishPriceDal.Add(fishPrice);

            return fish;
        }

        public async Task<int> Delete(Guid id)
        {
            return await _fishDal.Delete(id);
        }

        public async Task<List<Fish>> GetListAsync()
        {
            return await _fishDal.GetList();
        }

        public async Task<int> UpdateAsync(Fish fish)
        {
            return await _fishDal.Update(fish);
        }

        public async Task<List<Fish>> GetListAsNoTrackingAsync()
        {
            return await _fishDal.GetList();
        }
    }
}
