using FishMarket.Dto;
using FishMarket.Entities.Concrete;

namespace FishMarket.Service.Abstract
{
    public interface IFishService
    {
        Task<List<Fish>> GetListAsync();
        Task<List<Fish>> GetListAsNoTrackingAsync();
        Task<Fish> AddAsync(FishInsertDto fishInsertDto);
        Task<int> UpdateAsync(Fish fish);
        Task<int>Delete(Guid id);
    }
}
