using FishMarket.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace FishMarket.DataAccess.Concrete.EntityFrameworkCore
{
    public class FishMarketContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Settings.GetSettingValue("DBConnection", "MySQLConnectionString"),
                new MySqlServerVersion(new Version(5, 7, 37)));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet(CharSet.Utf8Mb4, DelegationModes.ApplyToAll);

            #region User EntityConfiguration
            modelBuilder.Entity<User>()
             .Ignore(i => i.CreatedBy)
             .Ignore(i => i.ChangedOn)
             .Ignore(i => i.ChangedBy);

            modelBuilder.Entity<User>().Property(e => e.Email).HasMaxLength(50);

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

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
            #endregion

            #region Fish EntityConfiguraiton
            //modelBuilder.Entity<FishPrice>()
            //.HasOne(f => f.Fish)
            //.WithMany(fp => fp.FishPrices).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Fish>()
            .HasMany(c => c.FishPrices)
            .WithOne(e => e.Fish).OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Fish>().Property(e => e.Type).HasMaxLength(50);

            modelBuilder.Entity<Fish>().HasIndex(u => u.Type).IsUnique();

            modelBuilder.Entity<FishPrice>().Property(e => e.Price).HasPrecision(10, 2);

            modelBuilder.Entity<FishPrice>()
              .Ignore(i => i.ChangedOn)
              .Ignore(i => i.ChangedBy);
            #endregion



            //modelBuilder.Entity<User>(UserConfiguration.ConfigureUserEntity);
            //modelBuilder.Entity<Fish>(FishConfiguration.ConfigureFishEntity);
            //modelBuilder.Entity<FishPrice>(FishPriceConfiguration.ConfigureFishPriceEntity);
        }

        public DbSet<Fish> Fish { get; set; }
        public DbSet<FishPrice> FishPrice { get; set; }
        public DbSet<User> User { get; set; }


    }
}
