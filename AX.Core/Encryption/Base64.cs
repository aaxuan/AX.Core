using AX.Core.Extension;
using System;
using System.Text;

namespace AX.Core.Encryption
{
    public static class Base64
    {
        public static string Decrypt(string value, Encoding encoding = null)
        {
            value.CheckIsNullOrWhiteSpace();
            if (encoding == null)
            { encoding = AxCoreGlobalSettings.Encodeing; }
            return encoding.GetString(Convert.FromBase64String(value));
        }

        public static string Encrypt(string value, Encoding encoding = null)
        {
            value.CheckIsNullOrWhiteSpace();
            if (encoding == null)
            { encoding = AxCoreGlobalSettings.Encodeing; }
            return Convert.ToBase64String(encoding.GetBytes(value));
        }
    }
}