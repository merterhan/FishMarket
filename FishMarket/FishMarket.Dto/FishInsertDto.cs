namespace FishMarket.Dto
{
    public class FishInsertDto
    {
        public string Type { get; set; }
        public decimal Price { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
