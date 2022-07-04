using FishMarket.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishMarket.DataAccess.Concrete.EntityFrameworkCore.Configurations
{
    public class UserConfiguration
    {
        public static void ConfigureUserEntity(EntityTypeBuilder<User> entityTypeBuilder)
        {
            entityTypeBuilder
             .Ignore(i => i.CreatedBy)
             .Ignore(i => i.ChangedOn)
             .Ignore(i => i.ChangedBy);

           entityTypeBuilder.Property(e => e.Email).HasMaxLength(50);

           entityTypeBuilder.HasIndex(u => u.Email).IsUnique();

           entityTypeBuilder
                .HasData(new User
                {
                    Id = Guid.NewGuid(),
                    Password = "F118A7EEF3641C3AC9E4D9176F5F1518D658AC674FFF113E22C7C9F74A2B7733",
                    PasswordSalt = "716c4bab-92b6-44c2-8d34-522357c5a685",
                    Email = "info@cagrierhan.com",
                    EmailConfirmed = true,
                    CreatedOn = DateTime.Now
                });
        }
    }
}
