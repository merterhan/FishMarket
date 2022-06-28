using FishMarket.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishMarket.DataAccess.Concrete.EntityFrameworkCore.Configurations
{
    public class FishConfiguration
    {
        public static void ConfigureFishEntity(EntityTypeBuilder<Fish> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Fish");

            entityTypeBuilder.HasKey(pk => pk.Id);
        }
    }
}
