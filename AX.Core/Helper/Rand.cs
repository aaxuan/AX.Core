using System;
using System.Security.Cryptography;
using System.Text;

namespace AX.Core.Helper
{
    public static class Rand
    {
        private static RandomNumberGenerator _rng;

        static Rand()
        {
            _rng = new RNGCryptoServiceProvider();
        }

        /// <summary>
        /// 获取 小于指定最大值的非负随机数
        /// </summary>
        /// <param name="max">随机数的上界（随机数不取该上界值）</param>
        /// <returns></returns>
        public static Int32 Next(Int32 max = Int32.MaxValue)
        {
            if (max <= 0) throw new ArgumentOutOfRangeException("max");
            return Next(0, max);
        }

        [ThreadStatic]
        private static Byte[] _buf;

        /// <summary>
        /// 返回一个指定范围内的随机数
        /// </summary>
        /// <param name="min">返回的随机数的下界（随机数可能取该下界值）</param>
        /// <param name="max">返回的随机数的上界（随机数不取该上界值）</param>
        /// <returns></returns>
        public static Int32 Next(Int32 min, Int32 max)
        {
            if (max <= min) throw new ArgumentOutOfRangeException("max");

            if (_buf == null) _buf = new Byte[4];
            _rng.GetBytes(_buf);

            var n = BitConverter.ToInt32(_buf, 0);
            if (min == Int32.MinValue && max == Int32.MaxValue) return n;
            if (min == 0 && max == Int32.MaxValue) return Math.Abs(n);
            if (min == Int32.MinValue && max == 0) return -Math.Abs(n);

            var num = max - min;
            // 不要进行复杂运算，看做是生成从0到(max-min)的随机数，然后再加上min即可
            return (Int32)((num * (UInt32)n >> 32) + min);
        }

        /// <summary>
        /// 返回指定长度随机字符串
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="symbol">是否包含符号</param>
        /// <returns></returns>
        public static String NextString(Int32 length, Boolean symbol = false)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var ch = ' ';
                if (symbol)
                    ch = (Char)Next(' ', 0x7F);
                else
                {
                    var n = Next(0, 10 + 26 + 26);
                    if (n < 10)
                        ch = (Char)('0' + n);
                    else if (n < 10 + 26)
                        ch = (Char)('A' + n - 10);
                    else
                        ch = (Char)('a' + n - 10 - 26);
                }
                sb.Append(ch);
            }

            return sb.ToString();
        }
    }
}