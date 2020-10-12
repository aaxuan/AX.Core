using AX.Core.Extension;
using System.Security.Cryptography;
using System.Text;

namespace AX.Core.Encryption
{
    public static class SHA1
    {
        public static string Encrypt(string value, Encoding encoding)
        {
            value.CheckIsNullOrWhiteSpace();
            var result = new StringBuilder();
            if (encoding == null)
            { encoding = AxCoreGlobalSettings.Encodeing; }
            var bytes = new SHA1Managed().ComputeHash(encoding.GetBytes(value));
            for (int i = 0; i < bytes.Length; i++)
            {
                result.Append(bytes[i].ToString("x").PadLeft(2, '0'));
            }
            return result.ToString();
        }
    }
}