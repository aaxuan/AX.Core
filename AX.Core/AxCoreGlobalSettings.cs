using System.Text;

namespace AX.Core
{
    public static class AxCoreGlobalSettings
    {
        public static bool IsDebug { get; set; } = false;

        public static bool DataBaseUseDapper { get; set; } = true;

        public static Encoding Encodeing { get; set; } = Encoding.UTF8;
    }
}