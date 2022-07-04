using FishMarket.DataAccess.Abstract;
using FishMarket.Dto;
using FishMarket.Entities.Concrete;
using FishMarket.Service.Abstract;

namespace FishMarket.Service.Concrete
{
    public class FishManager : IFishService
    {

        private readonly IFishDal _fishDal;
        private readonly IFishPriceDal _fishPriceDal;

        public FishManager(IFishDal fishDal, IFishPriceDal fishPriceDal)
        {
            _fishDal = fishDal;
            _fishPriceDal = fishPriceDal;
        }
        public async Task<Fish> AddAsync(FishInsertDto fishInsertDto)
        {
            var now = DateTime.Now;

            var fish = new Fish
            {
                Id = Guid.NewGuid(),
                Type = fishInsertDto.Type,
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
            var fishPrices = await _fishPriceDal.GetList(x => x.FishId == id);
            _fishPriceDal.RemoveRange(fishPrices);

            var result = await _fishDal.Delete(id);
            return result;
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

        public async Task<List<FishDto>> ListFishesAsync()
        {
            var result = (from f in await _fishDal.GetListAsNoTracking()
                          join p in await _fishPriceDal.GetListAsNoTracking() on f.Id equals p.FishId into fGroup
                          from p in fGroup.OrderByDescending(d => d.CreatedOn).Take(1)
                          select new FishDto
                          {
                              Id = f.Id,
                              Type = f.Type,
                              Price = p.Price
                          }).ToList();

            return result;
        }

        public async Task<Fish> GetByIdAsync(Guid fishId)
        {
            return await _fishDal.Get(w => w.Id == fishId);
        }
    }
}
