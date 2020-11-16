namespace AX.Core
{
    public static class AxCoreGlobalSettings
    {
        public static bool IsDebug { get; set; } = false;

        public static bool DataBaseUseDapper { get; set; } = true;

        public static string MailDefaultSubject = "系统通知";
    }
}