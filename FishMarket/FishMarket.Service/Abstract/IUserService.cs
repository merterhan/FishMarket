using FishMarket.Dto;
using FishMarket.Entities.Concrete;

namespace FishMarket.Service.Abstract
{
    public interface IUserService
    {
        Task<string> Login(UserLoginDto userLoginDto);
        Task<List<User>> GetList();
        Task<List<User>> GetListAsNoTracking();
        Task<User> Add(User user);
        Task<int> Update(User user);
        Task<int> Delete(Guid id);
    }
}
