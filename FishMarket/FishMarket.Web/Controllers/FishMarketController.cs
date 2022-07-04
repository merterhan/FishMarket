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
    public async Task<bool> Add(FishInsertDto fishInsertDto)
    {
        try
        {
            await _fishMarketClient.Insert(fishInsertDto);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    [HttpDelete]
    public async Task<bool> Delete(Guid fishId)
    {
        try
        {
            var result = await _fishMarketClient.DeleteFish(fishId);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        

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
    [HttpPost]
    public bool Edit(FishPriceUpdateDto model)
    {
        try
        {
            model.CreatedBy = _sessionService.GetUser().Id;
            model.CreatedOn = DateTime.Now;
            _fishMarketClient.UpdateFishPrice(model);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}