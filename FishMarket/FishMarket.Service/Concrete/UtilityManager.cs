using FishMarket.Service.Abstract;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;

namespace FishMarket.Service.Concrete
{
    public class UtilityManager : IUtilityService
    {
        private readonly IConfiguration _configuration;
        private string _encryptPublicKey;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        //private const string Key = "cut-the-night-with-the-light";
        public UtilityManager(IConfiguration configuration, IDataProtectionProvider dataProtectionProvider)
        {
            _configuration = configuration;
            _dataProtectionProvider = dataProtectionProvider;
            _encryptPublicKey = _configuration.GetSection("EncryptPublicKey").Value;
        }

        public Hashtable GetHashedPasswordWithSalt(string password)
        {
            var salt = Guid.NewGuid().ToString();

            password = password + salt;
            using (HashAlgorithm algorithm = SHA256.Create())
            {
                var bytePassword = algorithm.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytePassword)
                    sb.Append(b.ToString("X2"));

                password = sb.ToString();

            }
            return new Hashtable
            {
                { "salt", salt },
                { "hashedPassword", password }
            };
        }
        public bool VerifyPassword(string password, string salt, string hash)
        {
            password = password + salt;
            using (HashAlgorithm algorithm = SHA256.Create())
            {
                var bytePassword = algorithm.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytePassword)
                    sb.Append(b.ToString("X2"));

                password = sb.ToString();

            }

            return password == hash;
        }

        public string Encrypt(string input)
        {
            var protector = _dataProtectionProvider.CreateProtector(_encryptPublicKey);
            return protector.Protect(input);
        }

        public string Decrypt(string input)
        {
            var protector = _dataProtectionProvider.CreateProtector(_encryptPublicKey);
            return protector.Unprotect(input);
        }

    }
}
