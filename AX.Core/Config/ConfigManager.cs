using Microsoft.Extensions.Configuration;
using System.IO;

namespace AX.Core.Config
{
    public class ConfigManager
    {
        public static IConfiguration Config { get; set; }

        static ConfigManager()
        {
            Config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        }

        public static string GetValue(string key)
        {
            return Config[key];
        }

        public static string GetConnectionString(string nameOfConnectionString)
        {
            return Config.GetConnectionString(nameOfConnectionString);
        }
    }
}