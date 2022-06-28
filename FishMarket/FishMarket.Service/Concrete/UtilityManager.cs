using FishMarket.Service.Abstract;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;

namespace FishMarket.Service.Concrete
{
    public class UtilityManager : IUtilityService
    {
        IConfiguration _configuration;
        private readonly string _encryptPublicKey, _encryptSecretKey;
        public UtilityManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _encryptPublicKey = _configuration.GetSection("EncryptPublicKey").Value;
            _encryptSecretKey = _configuration.GetSection("EncryptSecretKey").Value;
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

        public string EncryptString(string textToEncrypt)
        {
            try
            {
                string ToReturn = string.Empty;
                string publickey = _encryptPublicKey, secretkey = _encryptSecretKey;

                byte[] secretkeyByte = { };
                secretkeyByte = Encoding.UTF8.GetBytes(secretkey);
                byte[] publickeybyte = { };
                publickeybyte = Encoding.UTF8.GetBytes(publickey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    ToReturn = Convert.ToBase64String(ms.ToArray());
                }
                return ToReturn;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }

            return string.Empty;
        }
        public string DecryptString(string textToDecrypt)
        {
            try
            {
                string ToReturn = "";
                string publickey = _encryptPublicKey;
                string secretkey = "87654321";
                byte[] privatekeyByte = { };
                privatekeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
                byte[] publickeybyte = { };
                publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = new byte[textToDecrypt.Replace(" ", "+").Length];
                inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    ToReturn = encoding.GetString(ms.ToArray());
                }
                return ToReturn;
            }
            catch (Exception ae)
            {
                throw new Exception(ae.Message, ae.InnerException);
            }
        }

    }
}
