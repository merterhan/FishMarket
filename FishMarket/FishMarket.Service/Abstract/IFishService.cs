using FishMarket.Dto;
using FishMarket.Entities.Concrete;

namespace FishMarket.Service.Abstract
{
    public interface IFishService
    {
        Task<Fish> GetByIdAsync(Guid fishId);
        Task<List<Fish>> GetListAsync();
        Task<List<Fish>> GetListAsNoTrackingAsync();
        Task<Fish> AddAsync(FishInsertDto fishInsertDto);
        Task<int> UpdateAsync(Fish fish);
        Task<int>Delete(Guid id);
        Task<List<FishDto>> ListFishesAsync();
    }
}
