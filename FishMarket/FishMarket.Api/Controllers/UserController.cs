using AutoMapper;
using FishMarket.Dto;
using FishMarket.Entities.Concrete;
using FishMarket.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FishMarket.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUtilityService _utilityService;
        

        private readonly ILogger<FishMarketController> _logger;
        private readonly IMapper _mapper;

        public UserController(IServiceScopeFactory serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                _logger = scope.ServiceProvider.GetRequiredService<ILogger<FishMarketController>>();
                _mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
                _userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                _utilityService = scope.ServiceProvider.GetRequiredService<IUtilityService>();
            }
        }

        [HttpGet, Route("GetToken")]
        public async Task<string> GetToken()
        {
            var user = new UserLoginDto
            {
                Email = "info@cagrierhan.com",
                Password = "12345"
            };
            var result =  await _userService.Login(user);
            return result.Token; 
        }

        [AllowAnonymous]
        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var token = await _userService.Login(userLoginDto);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost, Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            var model = _mapper.Map<User>(userRegisterDto);
            var result = await _userService.Add(model);
            return Ok(result);
        }
    }
}
