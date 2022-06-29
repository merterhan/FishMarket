using FishMarket.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace FishMarket.DataAccess.Concrete.EntityFrameworkCore
{
    public class FishMarketContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(@"Server=cagrierhan.com;Database=cagrierh_FishMarketDB;Uid=cagrierh_sa;Pwd=P2!ZW.n4;",
                new MySqlServerVersion(new Version(5, 7, 37)));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet(CharSet.Utf8Mb4, DelegationModes.ApplyToAll);

            modelBuilder.Entity<User>()
                .Ignore(i => i.CreatedBy)
                .Ignore(i => i.ChangedOn)
                .Ignore(i => i.ChangedBy);

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = Guid.NewGuid(),
                    Password = "F118A7EEF3641C3AC9E4D9176F5F1518D658AC674FFF113E22C7C9F74A2B7733",
                    PasswordSalt = "716c4bab-92b6-44c2-8d34-522357c5a685",
                    Email = "info@cagrierhan.com",
                    EmailConfirmed = true,
                    CreatedOn = DateTime.Now
                });

            modelBuilder.Entity<FishPrice>()
            .HasOne(f => f.Fish)
            .WithMany(fp => fp.FishPrices);

            modelBuilder.Entity<FishPrice>().Property(e => e.Price).HasPrecision(10, 2);

            modelBuilder.Entity<FishPrice>()
              .Ignore(i => i.CreatedBy)
              .Ignore(i => i.ChangedOn)
              .Ignore(i => i.ChangedBy);

            //modelBuilder.Entity<User>(UserConfiguration.ConfigureUserEntity);
            //modelBuilder.Entity<Fish>(FishConfiguration.ConfigureFishEntity);
            //modelBuilder.Entity<FishPrice>(FishPriceConfiguration.ConfigureFishPriceEntity);
        }

        public DbSet<Fish> Fish { get; set; }
        public DbSet<FishPrice> FishPrice { get; set; }
        public DbSet<User> User { get; set; }


    }
}
