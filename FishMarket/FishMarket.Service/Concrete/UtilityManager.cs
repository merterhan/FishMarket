using FishMarket.Service.Abstract;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Collections;
using System.Security.Cryptography;

namespace FishMarket.Service.Concrete
{
    public class UtilityManager : IUtilityService
    {
        public Hashtable GetHashedPasswordWithSalt(string password)
        {
            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return new Hashtable{
                { "salt", Convert.ToBase64String(salt) },
                { "hashedPassword", hashed }
            };
        }
    }
}
