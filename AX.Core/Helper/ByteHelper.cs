using AX.Core.Extension;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AX.Core.Helper
{
    public static class ByteHelper
    {
        public static byte[] ToByte(Object obj)
        {
            obj.CheckIsNull();
            MemoryStream ms = new MemoryStream();
            new BinaryFormatter().Serialize(ms, obj);
            byte[] buffer = ms.GetBuffer();
            return buffer;
        }

        public static T ToObject<T>(byte[] buffer) where T : class
        {
            MemoryStream ms = new MemoryStream(buffer, 0, buffer.Length, true, true);
            T result = new BinaryFormatter().Deserialize(ms) as T;
            return result;
        }
    }
}