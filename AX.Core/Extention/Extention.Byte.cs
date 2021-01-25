using AX.Core;
using System;
using System.Text;

namespace AX
{ 
    public static partial class Extention
    {
        public static string ToString(this byte[] bytes, Encoding encoding = null)
        {
            if (encoding == null)
            { encoding = GlobalDefaultSetting.Encoding; }
            return encoding.GetString(bytes);
        }

        public static string ToBase64String(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
    }
}