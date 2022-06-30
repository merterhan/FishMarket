using FishMarket.Client;
using FishMarket.Dto;
using FishMarket.Service.Abstract;
using FishMarket.Web.Models;
using FishMarket.Web.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FishMarket.Web.Controllers;

public class FishMarketController : Controller
{
    private readonly ILogger<FishMarketController> _logger;
    private readonly IFishMarketClient _fishMarketClient;
    private readonly IUserService _userService;
    private readonly ISessionService _sessionService;



    public FishMarketController(IServiceScopeFactory serviceScopeFactory)
    {
        using (var scope = serviceScopeFactory.CreateScope())
        {
            _logger = scope.ServiceProvider.GetRequiredService<ILogger<FishMarketController>>();
            _fishMarketClient = scope.ServiceProvider.GetRequiredService<IFishMarketClient>();
            _userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            _sessionService = scope.ServiceProvider.GetRequiredService<ISessionService>();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add(FishInsertDto fishInsertDto)
    {
        var result = await _fishMarketClient.Insert(fishInsertDto);
        return result;
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid fishId)
    {
        if (_sessionService.GetUser() != null)
        {
            var result = await _fishMarketClient.DeleteFish(fishId);
            return await Task.FromResult(result);
        }
        else
            return RedirectToAction("Login", "User");

    }
    [AllowAnonymous]
    public async Task<IActionResult> List()
    {
        var result = await _fishMarketClient.ListFishes();
        return View(result);
    }

    public async Task<IActionResult> Edit()
    {
        if (_sessionService.GetUser() != null)
        {
            var result = await _fishMarketClient.ListFishes();
            return View(result);
        }
        else
            return RedirectToAction("Login", "User");

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}