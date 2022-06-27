namespace FishMarket.Entities.Concrete
{
    public class Fish : BaseEntity
    {
        public string Type { get; set; }
        public ICollection<FishPrice> FishPrices { get; set; }
    }
}
