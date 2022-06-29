using FishMarket.Dto.ServiceResponseDtos;
using FishMarket.Web.ExtensionMethods;

namespace FishMarket.Web.Service;

public class SessionService : ISessionService
{
    private IHttpContextAccessor _httpContextAccessor;

    public SessionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public UserLoginResponseDto GetUser()
    {
        return _httpContextAccessor.HttpContext.Session.GetObject<UserLoginResponseDto>("user");
    }

    public void SetUser(UserLoginResponseDto user)
    {
        _httpContextAccessor.HttpContext.Session.SetObject("user", user);
    }
    public void ClearSession()
    {
        _httpContextAccessor.HttpContext.Session.Clear();
    }
}
