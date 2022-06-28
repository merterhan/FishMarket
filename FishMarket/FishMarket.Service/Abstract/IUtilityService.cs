using System.Collections;

namespace FishMarket.Service.Abstract
{
    public interface IUtilityService
    {
        Hashtable GetHashedPasswordWithSalt(string password);
        bool VerifyPassword(string password, string salt, string hash);
        string EncryptString(string textToEncrypt);
        string DecryptString(string textToDecrypt);

    }
}
