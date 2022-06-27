namespace FishMarket.Entities.Concrete
{
    public class FishPrice : BaseEntity
    {
        public Fish Fish { get; set; }        
        public decimal Price { get; set; }
    }
}
