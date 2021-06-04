using System;
using System.Security.Cryptography;
using System.Text;

namespace AX.Core.Encryption
{
    public static class SHA1
    {
        public static string Encrypt(string value, Encoding encoding)
        {
            if (value == null) { throw new ArgumentNullException(nameof(value)); }
            if (encoding == null) { encoding = GlobalDefaultSetting.Encoding; }
            var result = new StringBuilder();
            var bytes = new SHA1Managed().ComputeHash(encoding.GetBytes(value));
            for (int i = 0; i < bytes.Length; i++)
            {
                result.Append(bytes[i].ToString("x").PadLeft(2, '0'));
            }
            return result.ToString();
        }
    }
}