namespace AX.Core.DataBase.Config
{
    public static class GlobalConfig
    {
        public static int CommandTimeout { get; set; } = 50000;

        public static bool UseEscapeChar { get; set; } = true;

        public static bool TraceLogSql { get; set; } = true;
    }
}