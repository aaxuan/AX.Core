using System;

namespace AX
{
    public static partial class Extention
    {
        public static string ToByteSizeStr(this long size)
        {
            if (size <= 0)
            { return "0B"; }

            double result = size;
            var unit = string.Empty;

            if (result > 1024)
            {
                result = result / 1024;
                unit = "K";
                if (result > 1024)
                {
                    result = result / 1024;
                    unit = "M";
                    if (result > 1024)
                    {
                        result = result / 1024;
                        unit = "G";
                    }
                }
            }
            return string.Format("{0} {1}B", Math.Round(result, 2, MidpointRounding.AwayFromZero), unit);
        }
    }
}