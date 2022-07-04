using AutoMapper;
using FishMarket.Dto;
using FishMarket.Dto.ServiceResponseDtos;
using FishMarket.Entities.Concrete;
using FishMarket.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json;

namespace FishMarket.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUtilityService _utilityService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public UserController(IServiceScopeFactory serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                _logger = scope.ServiceProvider.GetRequiredService<ILogger<UserController>>();
                _mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
                _userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                _utilityService = scope.ServiceProvider.GetRequiredService<IUtilityService>();
                _configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            }
        }

        /// <summary>
        /// Retrieves a token with authorized admin user for test
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetToken")]
        public async Task<string> GetToken()
        {
            var user = new UserLoginDto
            {
                Email = _utilityService.Decrypt(_configuration.GetSection("AdminUserEmail").Value),
                Password = _utilityService.Decrypt(_configuration.GetSection("AdminUserPassword").Value)
            };

            var result = await _userService.Login(user);
            _logger.LogInformation(String.Concat("Bearer Token Generated: ", result.Token));
            return result.Token;
        }

        /// <summary>
        /// Retrieves a UserLoginResponseDto which includes also jwt token
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserLoginResponseDto), 200)]
        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var result = await _userService.Login(userLoginDto);
            if (string.IsNullOrEmpty(result.Token) && !result.IsSuccess)
            {
                _logger.LogError(JsonSerializer.Serialize(result));
                return BadRequest(result.Message);
            }

            _logger.LogInformation(JsonSerializer.Serialize(result));
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a Email Confirmaton Link
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            try
            {
                var model = _mapper.Map<User>(userRegisterDto);
                var result = await _userService.Add(model);
                _logger.LogInformation($"Go to this link to confirm your email: {result}");
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
