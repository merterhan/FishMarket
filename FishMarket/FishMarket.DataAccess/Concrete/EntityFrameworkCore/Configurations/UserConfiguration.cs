using FishMarket.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishMarket.DataAccess.Concrete.EntityFrameworkCore.Configurations
{
    public class UserConfiguration
    {
        public static void ConfigureUserEntity(EntityTypeBuilder<User> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("User");

            entityTypeBuilder
                .Ignore(i => i.CreatedBy)
                .Ignore(i => i.ChangedOn)
                .Ignore(i => i.ChangedBy);

            entityTypeBuilder
               .HasData(new User
               {
                   Id = Guid.NewGuid(),
                   Password = "12345",
                   PasswordSalt = String.Empty,
                   Email = "info@cagrierhan.com",
                   EmailConfirmed = true,
                   CreatedOn = DateTime.Now
               });
        }
    }
}
