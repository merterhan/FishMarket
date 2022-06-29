namespace FishMarket.Dto.ServiceResponseDtos
{
    public class FishPriceUpdateApiResponseDto : BaseResponse
    {
        public Guid Id { get; set; }
        public decimal? Price { get; set; }
    }
}
