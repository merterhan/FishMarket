using FishMarket.DataAccess.Abstract;
using FishMarket.Dto;
using FishMarket.Dto.ServiceResponseDtos;
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
        public async Task<string> Add(User user)
        {
            try
            {
                var passwordWithSalt = _utilityService.GetHashedPasswordWithSalt(user.Password);

                user.Password = passwordWithSalt["hashedPassword"].ToString();

                user.PasswordSalt = passwordWithSalt["salt"].ToString();

                var result = await _userDal.Add(user);

                var token = _tokenManager.GetToken(user.Email);

                var confirmAccountLink = new Uri("http://localhost:5067/User/ConfirmEmail?email=" + user.Email + "&token=" + token);

                return confirmAccountLink.ToString();
            }
            catch (Exception e)
            {
                return e.Message;
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

        public async Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
        {
            var user = await _userDal.Get(w => w.Email == userLoginDto.Email);

            if (user == null)
            {
                return new UserLoginResponseDto
                {
                    IsSuccess = false,
                    Message = "User Not Found"
                };
            }
            else
            {
                var hashedPassword = _utilityService.VerifyPassword(userLoginDto.Password, user.PasswordSalt, user.Password);
                switch (hashedPassword)
                {
                    case true:
                        return new UserLoginResponseDto
                        {
                            Id = user.Id,
                            Email = user.Email,
                            Token = _tokenManager.GetToken(user.Email),
                            IsSuccess = true,
                            Message = "Login Successful"
                        };
                    case false:
                        return new UserLoginResponseDto
                        {
                            IsSuccess = false,
                            Message = "Wrong Password"
                        };
                }

            }

        }

        public Task<User> Get(Guid userId)
        {
            return _userDal.Get(w => w.Id == userId);
        }

        public async Task<BaseResponse> ConfirmUserEmail(string email)
        {
            try
            {
                var user = await _userDal.Get(w => w.Email == email);
                user.EmailConfirmed = true;
                var result = await _userDal.Update(user);

                if (result != 0)
                {
                    return new BaseResponse { IsSuccess = true, Message = "Email Confirmed" };
                }
                else
                {
                    return new BaseResponse { IsSuccess = true, Message = "Email Confirmation Failed" };
                }
            }
            catch (Exception e)
            {

                return new BaseResponse { IsSuccess = false, Message = e.Message };
            }
        }
    }
}
