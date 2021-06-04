using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AX.Core.Config
{
    public class ConfigManager
    {
        public static IConfiguration Config { get; set; }

        static ConfigManager()
        {
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json")) == false) { return; }
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

        public static List<T> GetArrayValue<T>(string key)
        {
            return Config.GetSection(key).Get<T[]>().ToList();
        }
    }
}