namespace FishMarket.Dto.ServiceResponseDtos
{
    public class FishPriceUpdateApiResponseDto : BaseApiResonse
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public decimal? Price { get; set; }
        public DateTime? ChangedOn { get; set; }
    }
}
