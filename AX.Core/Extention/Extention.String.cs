using System;
using System.Text;

namespace AX
{
    public static partial class Extention
    {
        /// <summary>
        /// 计算字符串长度 汉字长度为 2
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>长度</returns>
        public static int GetCherLength(this String value)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            var result = 0;
            var s = ascii.GetBytes(value);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                    result += 2;
                else
                    result += 1;
            }
            return result;
        }

        /// <summary>
        /// 拆分字符串，
        /// 分组分隔符，默认中英逗号 过滤空格，
        /// 会返回空数组
        /// </summary>
        public static String[] Split(this String value, params String[] separators)
        {
            if (String.IsNullOrWhiteSpace(value))
            { return new String[0]; }

            if (separators == null)
            { separators = new String[] { ",", "，" }; }

            return value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}