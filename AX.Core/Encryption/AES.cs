using AX.Core.Extension;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AX.Core.Encryption
{
    public static class AES
    {
        //ECB 模式 无需密钥

        ////默认密钥向量
        //private static readonly byte[] Keys = { 0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79, 0x53, 0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F };

        ////默认密钥
        //private static readonly String CryptKey = "axframework                     ";

        ///// <summary>
        ///// 解密
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static string Decrypt(string value)
        //{
        //    value.CheckIsNullOrWhiteSpace();

        // try { //decryptKey = LZUtils.GetSubString(decryptKey, 32, ""); //decryptKey =
        // decryptKey.PadRight(32, ' ');

        // var rijndaelProvider = new RijndaelManaged(); rijndaelProvider.Key =
        // Encoding.UTF8.GetBytes(CryptKey); rijndaelProvider.IV = Keys; var rijndaelDecrypt = rijndaelProvider.CreateDecryptor();

        // byte[] inputData = Convert.FromBase64String(value); byte[] decryptedData =
        // rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);

        //        return Encoding.UTF8.GetString(decryptedData);
        //    }
        //    catch
        //    {
        //        return string.Empty;
        //    }
        //}

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="value"> </param>
        /// <returns> </returns>
        public static string Encrypt(byte[] key, byte[] iv, string value)
        {
            value.CheckIsNullOrWhiteSpace();
            var result = new StringBuilder();
            byte[] bytes = null;
            using (Aes aes = Aes.Create())
            {
                using (MemoryStream msbase = new MemoryStream())
                {
                    using (CryptoStream cstr = new CryptoStream(msbase, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(cstr))
                        {
                            writer.Write(value);
                        }
                    }
                    bytes = msbase.ToArray();
                }
            }

            for (int i = 0; i < bytes.Length; i++)
            {
                result.Append(bytes[i].ToString("x").PadLeft(2, '0'));
            }
            return result.ToString();
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="value"> </param>
        /// <returns> </returns>
        public static string Decrypt(byte[] key, byte[] iv, string value)
        {
            value.CheckIsNullOrWhiteSpace();
            var result = new StringBuilder();
            using (Aes aes = Aes.Create())
            {
                using (MemoryStream ms = new MemoryStream(AxCoreGlobalSettings.Encodeing.GetBytes(value)))
                {
                    ICryptoTransform trf = aes.CreateDecryptor(key, iv);
                    using (CryptoStream cstr = new CryptoStream(ms, trf, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cstr))
                        {
                            result.Append(reader.ReadToEnd());
                        }
                    }
                }
            }
            return result.ToString();
        }
    }
}