using System;
using System.Text;

namespace AX.Core.Encryption
{
    public static class Base64
    {
        public static string Decrypt(string value, Encoding encoding = null)
        {
            if (value == null) { throw new ArgumentNullException(nameof(value)); }
            if (encoding == null)
            { encoding = GlobalDefaultSetting.Encoding; }
            return encoding.GetString(Convert.FromBase64String(value));
        }

        public static string Encrypt(string value, Encoding encoding = null)
        {
            if (value == null) { throw new ArgumentNullException(nameof(value)); }
            if (encoding == null)
            { encoding = GlobalDefaultSetting.Encoding; }
            return Convert.ToBase64String(encoding.GetBytes(value));
        }
    }
}