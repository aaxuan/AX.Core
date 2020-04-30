using AX.Core.Extension;
using System;
using System.Security.Cryptography;
using System.Text;

namespace AX.Core.Encryption
{
    public static class SHA1
    {
        public static string Encrypt(string value, Encoding encoding)
        {
            value.CheckIsNullOrWhiteSpace();
            if (encoding == null)
            { encoding = Encoding.UTF8; }
            return Convert.ToBase64String(new SHA1Managed().ComputeHash(encoding.GetBytes(value)));
        }
    }
}