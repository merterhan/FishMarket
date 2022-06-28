namespace FishMarket.Tests;

public class FishMarketTest
{
    public Mock<IFishService> mock = new Mock<IFishService>();
    private readonly IFishPriceService _fishPriceService;
    public FishMarketTest(IServiceScopeFactory serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            _fishPriceService = scope.ServiceProvider.GetRequiredService<IFishPriceService>();
        }
    }

    [Fact]
    public async void FishPriceShouldBeUpdate()
    {
        var fishUpdateDto = new FishPriceUpdateDto
        {
            FishId = Guid.NewGuid(),
            Price = 100
        };
        var result = await _fishPriceService.UpdateFishPriceAsync(fishUpdateDto);

        Assert.Equal(fishUpdateDto.Price, result.Price);
    }
}