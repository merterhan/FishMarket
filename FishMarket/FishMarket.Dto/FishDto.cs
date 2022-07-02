using System.ComponentModel.DataAnnotations;

namespace FishMarket.Dto
{
    public class FishDto
    {
        public Guid? Id { get; set; }
        public string Type { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? Price { get; set; }
    }
}
