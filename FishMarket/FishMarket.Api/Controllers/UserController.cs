using AutoMapper;
using FishMarket.Dto;
using FishMarket.Entities.Concrete;
using FishMarket.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FishMarket.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly ILogger<FishMarketController> _logger;
        private readonly IMapper _mapper;

        public UserController(IUserService us, ILogger<FishMarketController> il, IMapper m)
        {
            _userService = us;
            _logger = il;
            _mapper = m;
       
            //using (var scope = serviceProvider.CreateScope())
            //{
            //    _userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            //    _logger = scope.ServiceProvider.GetRequiredService<ILogger<FishMarketController>>();
            //    _mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            //}
        }

        [HttpPost, Route("GetToken")]
        public async Task<IActionResult> GetToken()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                CreatedOn = DateTime.Now,
                Email = "info@cagrierhan.com"
            };
            await _userService.Add(user);
            return Ok(user);
        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var token = _userService.Login(userLoginDto);
            //await _fishManager.Add(fish);
            return Ok(token);
        }


        
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost, Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            var model = _mapper.Map<User>(userRegisterDto);
            var result = await _userService.Add(model);
            return Ok(result);
        }
    }
}
