using System.Collections;

namespace FishMarket.Service.Abstract
{
    public interface IUtilityService
    {
        Hashtable GetHashedPasswordWithSalt(string password);
    }
}
