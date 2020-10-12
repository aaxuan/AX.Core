using AX.Core.Extension;
using System.Security.Cryptography;
using System.Text;

namespace AX.Core.Encryption
{
    public static class MD5
    {
        public static string Encrypt(string value, Encoding encodeing = null)
        {
            value.CheckIsNullOrWhiteSpace();
            if (encodeing == null)
            { encodeing = AxCoreGlobalSettings.Encodeing; }
            var bytes = new MD5CryptoServiceProvider().ComputeHash(encodeing.GetBytes(value));
            var result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                result.Append(bytes[i].ToString("x").PadLeft(2, '0'));
            }
            return result.ToString();
        }
    }
}