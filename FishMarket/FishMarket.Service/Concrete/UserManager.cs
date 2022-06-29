using FishMarket.DataAccess.Abstract;
using FishMarket.Dto;
using FishMarket.Entities.Concrete;
using FishMarket.Service.Abstract;

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
            try
            {
                var passwordWithSalt = _utilityService.GetHashedPasswordWithSalt(user.Password);

                user.Password = passwordWithSalt["hashedPassword"].ToString();

                user.PasswordSalt = passwordWithSalt["salt"].ToString();

                var result = await _userDal.Add(user);
                //if (result.Succeeded)
                //{

                //}
                return user;
            }
            catch (Exception e)
            {
                throw e;
            }

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
            var user = await _userDal.Get(w => w.Email == userLoginDto.Email);

            if (user == null)
            {
                return "User Not Found";
            }
            else
            {
                var hashedPassword = _utilityService.VerifyPassword(userLoginDto.Password, user.PasswordSalt, user.Password);
                switch (hashedPassword)
                {
                    case true:
                        return _tokenManager.GetToken(user.Email);
                    case false:
                        return "Wrong Password";
                }

            }

        }

        public Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> Get(Guid userId)
        {
            return _userDal.Get(w => w.Id == userId);
        }

        public void ConfirmEmailAsync(User user, string token)
        {
            throw new NotImplementedException();
        }
    }
}
