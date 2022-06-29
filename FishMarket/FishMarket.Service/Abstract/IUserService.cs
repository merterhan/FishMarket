using FishMarket.Dto;
using FishMarket.Dto.ServiceResponseDtos;
using FishMarket.Entities.Concrete;

namespace FishMarket.Service.Abstract
{
    public interface IUserService
    {
        Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto);
        Task<List<User>> GetList();
        Task<List<User>> GetListAsNoTracking();
        Task<string> Add(User user);
        Task<int> Update(User user);
        Task<int> Delete(Guid id);
        Task<User> Get(Guid userId);

        Task<string> GenerateEmailConfirmationTokenAsync(User user);
        Task<BaseResponse> ConfirmUserEmail(string email);
    }
}
