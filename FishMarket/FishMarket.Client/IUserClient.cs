using FishMarket.Dto;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace FishMarket.Client
{
    public interface IUserClient
    {
        [Post("/User/Login")]
        Task<IActionResult> Login([Body] UserLoginDto userLoginDto);

        [Post("/User/Register")]
        Task<IActionResult> Register([Body] UserRegisterDto userRegisterDto);
    }
}