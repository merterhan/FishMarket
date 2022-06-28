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
               .HasOne(f => f.Fish)
               .WithMany(fp => fp.FishPrices);

            //entityTypeBuilder
            //   .HasOne(f => f.Fish)
            //   .WithMany(fp => fp.FishPrices);
        }
    }
}
