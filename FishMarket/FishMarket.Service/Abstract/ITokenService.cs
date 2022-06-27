namespace FishMarket.Service.Abstract
{
    public interface ITokenService
    {
        string GetToken(string email);
        int? ValidateToken(string token);
    }
}
