using Microsoft.Extensions.Configuration;

namespace FishMarket.DataAccess;


public class Settings
{
    #region Methods
    public static string GetSettingValue(string MainKey, string SubKey)
    {
        return Configuration.GetSection(MainKey).GetValue<string>(SubKey);
    }
    #endregion

    #region Properties
    public static IConfigurationRoot _configuration;
    public static IConfigurationRoot Configuration
    {
        get
        {
            if (_configuration == null)
            {
                IConfigurationBuilder builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables();
                _configuration = builder.Build();
            }
            return _configuration;
        }
    }
    #endregion

}
