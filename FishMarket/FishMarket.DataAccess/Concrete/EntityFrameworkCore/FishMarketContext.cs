using FishMarket.DataAccess.Concrete.EntityFrameworkCore.Configurations;
using FishMarket.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace FishMarket.DataAccess.Concrete.EntityFrameworkCore
{
    public class FishMarketContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder = new DbContextOptionsBuilder<EFFishDal>();
            optionsBuilder.UseMySql(@"Server=cagrierhan.com;Database=cagrierh_FishMarketDB;Uid=cagrierh_sa;Pwd=P2!ZW.n4;", new MySqlServerVersion(new Version(5, 7, 37)));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(UserConfiguration.ConfigureUserEntity);
            modelBuilder.Entity<FishPrice>(FishPriceConfiguration.ConfigureFishPriceEntity);
        }

        public DbSet<Fish> Fish { get; set; }
        public DbSet<FishPrice> FishPrice { get; set; }
        public DbSet<User> User { get; set; }


    }
}
