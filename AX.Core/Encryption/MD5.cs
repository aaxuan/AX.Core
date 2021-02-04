using System;
using System.Security.Cryptography;
using System.Text;

namespace AX.Core.Encryption
{
    public static class MD5
    {
        public static string Encrypt(string value, Encoding encoding = null)
        {
            if (value == null) { throw new ArgumentNullException(nameof(value)); }
            if (encoding == null)
            { encoding = GlobalDefaultSetting.Encoding; }
            var bytes = new MD5CryptoServiceProvider().ComputeHash(encoding.GetBytes(value));
            var result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                result.Append(bytes[i].ToString("x").PadLeft(2, '0'));
            }
            return result.ToString();
        }
    }
}