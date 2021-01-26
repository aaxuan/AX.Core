using System.Reflection;
using System.Text;

namespace AX.Core
{
    public class GlobalDefaultSetting
    {
        public static bool IsDebug { get; set; } = false;

        public static bool UseFileLog { get; set; } = false;

        public static Encoding Encoding { get; set; } = Encoding.UTF8;

        public static BindingFlags BindingFlags { get; set; } = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static;
    }
}