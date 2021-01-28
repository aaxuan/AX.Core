using Serilog;
using System;
using System.Collections;
using System.Text;

namespace AX.Core.Log
{
    public class LogManager
    {
        static LogManager()
        {
            var config = new LoggerConfiguration();
            config.WriteTo.Console();

            //最小日志等级
            if (GlobalDefaultSetting.IsDebug)
            { config.MinimumLevel.Debug(); }
            else
            { config.MinimumLevel.Information(); }
            //是否输出到文件
            if (GlobalDefaultSetting.UseFileLog)
            { config.WriteTo.File("logs\\log-.txt", rollingInterval: RollingInterval.Day); }

            Serilog.Log.Logger = config.CreateLogger();
        }

        public static string LogEnvironmentInfo()
        {
            var result = new StringBuilder();
            result.AppendLine($"Net Version： {Environment.Version}");
            result.AppendLine($"OS Version： {Environment.OSVersion}");
            result.AppendLine($"MachineName： {Environment.MachineName}");

            result.AppendLine($"Is64BitProcess： {Environment.Is64BitProcess}");
            result.AppendLine($"Is64BitOperatingSystem： {Environment.Is64BitOperatingSystem}");
            result.AppendLine($"CurrentProcessValues：");
            IDictionary environmentVariables = Environment.GetEnvironmentVariables();
            foreach (DictionaryEntry dictionaryEntry in environmentVariables)
            { result.AppendLine($" {dictionaryEntry.Key} = {dictionaryEntry.Value}"); }

            Serilog.Log.Warning(result.ToString());
            return result.ToString();
        }
    }
}