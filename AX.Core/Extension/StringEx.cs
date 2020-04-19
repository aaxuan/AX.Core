using System;
using System.Collections.Generic;
using System.Text;

namespace AX.Core.Extension
{
    public static class StringEx
    {
        /// <summary>
        /// 如果为空 则抛异常
        /// </summary>
        /// <param name="str">字符串</param>
        public static void CheckIsNullOrWhiteSpace(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            { throw new NullReferenceException(); }
        }

        /// <summary>
        /// 添加  Environment.NewLine
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AddNewLine(this string str)
        {
            return str += Environment.NewLine;
        }

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

        /// <summary>
        /// 拆分字符串成为整型数组
        /// 分组分隔符，默认逗号
        /// 过滤空格、过滤无效、不过滤重复
        /// </summary>
        public static List<int> SplitAsInt(this String value, params String[] separators)
        {
            var result = new List<int>();
            int id = 0;

            if (String.IsNullOrWhiteSpace(value))
            { return result; }

            if (separators == null || separators.Length < 1)
            { separators = new String[] { ",", "，" }; }

            var ss = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in ss)
            {
                if (!int.TryParse(item.Trim(), out id))
                { continue; }
                result.Add(id);
            }

            return result;
        }
    }
}