using FishMarket.Core;
using System.ComponentModel.DataAnnotations;

namespace FishMarket.Entities
{
    public class BaseEntity : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ChangedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? ChangedBy { get; set; }
    }
}
