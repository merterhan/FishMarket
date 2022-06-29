namespace FishMarket.Service.Abstract
{
    public interface ITokenService
    {
        string GetToken(string email);
        bool ValidateToken(string token, string email);
    }
}
