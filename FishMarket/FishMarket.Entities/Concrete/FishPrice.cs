using System.ComponentModel.DataAnnotations.Schema;

namespace FishMarket.Entities.Concrete
{
    public class FishPrice : BaseEntity
    {
        public virtual Fish Fish { get; set; }

        [ForeignKey("FishId")]
        public Guid FishId { get; set; }
        public decimal Price { get; set; }
    }
}
