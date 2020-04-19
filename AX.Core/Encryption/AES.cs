
using System;
using System.Security.Cryptography;
using System.Text;

namespace AX.Framework.Encryption
{
    public static class AES
    {
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

        //    try
        //    {
        //        //decryptKey = LZUtils.GetSubString(decryptKey, 32, "");
        //        //decryptKey = decryptKey.PadRight(32, ' ');

        //        var rijndaelProvider = new RijndaelManaged();
        //        rijndaelProvider.Key = Encoding.UTF8.GetBytes(CryptKey);
        //        rijndaelProvider.IV = Keys;
        //        var rijndaelDecrypt = rijndaelProvider.CreateDecryptor();

        //        byte[] inputData = Convert.FromBase64String(value);
        //        byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);

        //        return Encoding.UTF8.GetString(decryptedData);
        //    }
        //    catch
        //    {
        //        return string.Empty;
        //    }
        //}

        ///// <summary>
        ///// 加密
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static string Encrypt(string value)
        //{
        //    value.CheckIsNullOrWhiteSpace();
        //    //encryptKey = LZUtils.GetSubString(encryptKey, 32, "");
        //    //encryptKey = encryptKey.PadRight(32, ' ');

        //    var rijndaelProvider = new RijndaelManaged();
        //    //32位key？
        //    rijndaelProvider.Key = Encoding.UTF8.GetBytes(CryptKey);
        //    rijndaelProvider.IV = Keys;
        //    ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();

        //    byte[] inputData = Encoding.UTF8.GetBytes(value);
        //    byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);

        //    return Convert.ToBase64String(encryptedData);
        //}
    }
}