using FishMarket.DataAccess.Concrete.EntityFrameworkCore;
using FishMarket.Dto;
using FishMarket.Service.Concrete;

namespace FishMarket.Tests;

public class FishMarketTest
{   
    [Fact]
    public async void Fish_Sholud_Added()
    {
        FishManager service = new FishManager(new EFFishDal(), new EFFishPriceDal());
        var model = new FishInsertDto
        {
            Type = "Tuna",
            Price = 42
        }; 
        var result = await service.AddAsync(model);
        
        Assert.NotNull(result);
    }

    [Fact]
    public void Token_Should_Generate_Valid()
    {
        var testEmail = "test@cagrierhan.com";
        var service = new TokenManager();
        var token =  service.GetToken(testEmail);
        var isValid = service.ValidateToken(token, testEmail);
        Assert.NotNull(token);
        Assert.True(isValid);
    }
}