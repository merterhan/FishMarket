using FishMarket.Dto.ServiceResponseDtos;
namespace FishMarket.Web.Service;

public interface ISessionService
{
    UserLoginResponseDto GetUser();
    void SetUser(UserLoginResponseDto user);
    void ClearSession();
}
