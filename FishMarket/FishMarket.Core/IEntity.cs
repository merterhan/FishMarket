using System.ComponentModel.DataAnnotations;

namespace FishMarket.Core
{
    public interface IEntity
    {
        [Key]
        Guid Id { get; }
        DateTime CreatedOn { get; set; }
        DateTime? ChangedOn { get; set; }
        Guid CreatedBy { get; set; }
        Guid? ChangedBy { get; set; }
    }
}
