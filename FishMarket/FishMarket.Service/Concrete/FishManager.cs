using FishMarket.DataAccess.Abstract;
using FishMarket.Entities.Concrete;
using FishMarket.Service.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace FishMarket.Service.Concrete
{
    public class FishManager : IFishService
    {

        private readonly IFishDal _fishDal;

        public FishManager(IServiceScopeFactory serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                _fishDal = scope.ServiceProvider.GetRequiredService<IFishDal>();
            }
        }
        public async Task<Fish> Add(Fish fish)
        {
            await _fishDal.Add(fish);
            return fish;
        }

        public async Task<int> Delete(Guid id)
        {
            return await _fishDal.Delete(id);
        }

        public async Task<List<Fish>>  GetList()
        {
            return await _fishDal.GetList();
        }

        public async Task<int> Update(Fish fish)
        {
            return await _fishDal.Update(fish);
        }

        public async Task<List<Fish>> GetListAsNoTracking()
        {
            return await _fishDal.GetList();
        }


    }
}
