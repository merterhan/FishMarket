using FishMarket.Entities.Concrete;

namespace FishMarket.Service.Abstract
{
    public interface IFishService
    {
        Task<List<Fish>> GetList();
        Task<List<Fish>> GetListAsNoTracking();
        Task<Fish> Add(Fish department);
        Task<int> Update(Fish department);
        Task<int>Delete(Guid id);
    }
}
