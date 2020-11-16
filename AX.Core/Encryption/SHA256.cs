using AX.Core.Extension;
using System;
using System.Security.Cryptography;
using System.Text;

namespace AX.Core.Encryption
{
    public static class SHA256
    {
        public static string Encrypt(string value, Encoding encoding)
        {
            value.CheckIsNullOrWhiteSpace();
            if (encoding == null)
            { encoding = AxCoreGlobalSettings.Encodeing; }
            return Convert.ToBase64String(new SHA256Managed().ComputeHash(encoding.GetBytes(value)));
        }
    }
}