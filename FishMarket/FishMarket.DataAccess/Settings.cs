namespace FishMarket.DataAccess
{
    public class Settings
    {
        public string ConnectionString { get; }

        public Settings()
        {
            ConnectionString = @"Server=cagrierhan.com;Database=cagrierh_FishMarketDB;Uid=cagrierh_sa;Pwd=P2!ZW.n4;";
        }
        
    }
}
