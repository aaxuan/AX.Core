using System;
using System.Security.Cryptography;
using System.Text;

namespace AX.Core.Encryption
{
    public static class SHA256
    {
        public static string Encrypt(string value, Encoding encoding)
        {
            if (value == null) { throw new ArgumentNullException(nameof(value)); }
            if (encoding == null)
            { encoding = GlobalDefaultSetting.Encoding; }
            return Convert.ToBase64String(new SHA256Managed().ComputeHash(encoding.GetBytes(value)));
        }
    }
}