using FishMarket.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishMarket.DataAccess.Concrete.EntityFrameworkCore.Configurations
{
    public class FishPriceConfiguration
    {
        public static void ConfigureFishPriceEntity(EntityTypeBuilder<FishPrice> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("FishPrice");

            entityTypeBuilder
                .Ignore(i => i.CreatedBy)
                .Ignore(i => i.ChangedOn)
                .Ignore(i => i.ChangedBy);

            entityTypeBuilder
               .HasOne(f => f.Fish)
               .WithMany(fp => fp.FishPrices);
        }
    }
}
