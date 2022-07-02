namespace FishMarket.Dto
{
    public class FishPriceUpdateDto
    {
        public Guid FishId { get; set; }
        public decimal Price { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
