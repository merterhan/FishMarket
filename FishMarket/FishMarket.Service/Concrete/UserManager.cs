using FishMarket.DataAccess.Abstract;
using FishMarket.Dto;
using FishMarket.Entities.Concrete;
using FishMarket.Service.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace FishMarket.Service.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly ITokenService _tokenManager;
        private readonly IUtilityService _utilityService;

        public UserManager(ITokenService tokenManager, IUserDal userDal, IUtilityService utilityService)
        {
            _tokenManager = tokenManager;
            _userDal = userDal;
            _utilityService = utilityService;
        }
       
        
        public async Task<User> Add(User user)
        {
            var passwordWithSalt = _utilityService.GetHashedPasswordWithSalt(user.Password);
            user.Password = (string)passwordWithSalt["hashedPassword"];
            user.Password = (string)passwordWithSalt["salt"];

            await _userDal.Add(user);
            return user;
        }

        public async Task<int> Delete(Guid id)
        {
            return await _userDal.Delete(id);
        }

        public async Task<List<User>> GetList()
        {
            return await _userDal.GetList();
        }

        public async Task<int> Update(User user)
        {
            return await _userDal.Update(user);
        }

        public async Task<List<User>> GetListAsNoTracking()
        {
            return await _userDal.GetList();
        }

        public async Task<string> Login(UserLoginDto userLoginDto)
        {
            var user = await _userDal.Get(w => w.Email == userLoginDto.Email && w.Password == userLoginDto.Password);
            if (user != null)
            {
                return _tokenManager.GetToken(user.Email);
            }
            return null;
        }
    }
}
