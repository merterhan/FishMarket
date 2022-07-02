using AutoMapper;
using FishMarket.Api.Controllers;
using FishMarket.DataAccess.Concrete.EntityFrameworkCore;
using FishMarket.Dto.ServiceResponseDtos;
using FishMarket.Service.Concrete;

namespace FishMarket.Tests;

public class FishMarketTest
{
   
    [Fact]
    public async void Fishes_Shold_Be_Listing()
    {
        var mock = new Mock<IFishService>();

        var serviceProvider = new Mock<IServiceProvider>();
        //TODO serviceProvider.Setup with FishManager??
        serviceProvider
            .Setup(x => x.GetService(typeof(FishManager)))
            .Returns(mock);

        var serviceScope = new Mock<IServiceScope>();
        serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);
        var serviceScopeFactory = new Mock<IServiceScopeFactory>();
        serviceScopeFactory.Setup(s => s.CreateScope()).Returns(serviceScope.Object);

        var mockUser = new Mock<IUserService>();

        var controller = new FishMarketController(serviceScopeFactory.Object);
        var result = await controller.ListFishes();

        Assert.NotNull(result);
    }
}