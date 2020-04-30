using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AX.Core.Encryption
{
    public static class DES
    {
        public static string Encrypt(string data, string key, Encoding encoding)
        {
            key = key.Substring(0, 8);
            if (encoding == null)
            { encoding = Encoding.UTF8; }
            byte[] byteData = encoding.GetBytes(data);
            byte[] byteKey = encoding.GetBytes(key);
            DESCryptoServiceProvider desProcider = new DESCryptoServiceProvider();
            //DES一共有电子密码本模式（ECB）、加密分组链接模式（CBC）、加密反馈模式（CFB）和输出反馈模式（OFB）四种模式
            desProcider.Mode = CipherMode.ECB;
            //desProcider.Padding = PaddingMode.PKCS7;
            desProcider.Key = byteKey;
            desProcider.IV = byteKey;

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, desProcider.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(byteData, 0, byteData.Length);
            cs.FlushFinalBlock();
            var result = Convert.ToBase64String(ms.ToArray());
            return result;
        }
    }
}