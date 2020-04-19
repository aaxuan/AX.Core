using System;

namespace AX.Core.Extension
{
    public static class DateTimeEx
    {
        /// <summary>
        /// 格式化输出枚举
        /// </summary>
        public enum EnumFormatMode
        {
            yyyy_MM,
            yyyy年MM月,
            MM_dd,
            yyyy_MM_dd,
            MM月dd日,
            yyyy年MM月dd日,
            yyyy_MM_dd_HH_mm_ss,
            yyyy年MM月dd日HH时mm分ss秒,
        }

        /// <summary>
        /// 格式化日期时间
        /// </summary>
        public static string Format(this DateTime date, EnumFormatMode enumFormat)
        {
            switch (enumFormat)
            {
                case EnumFormatMode.yyyy_MM_dd: return date.ToString("yyyy-MM-dd");
                case EnumFormatMode.yyyy年MM月dd日: return date.ToString("yyyy年MM月dd日");
                case EnumFormatMode.MM_dd: return date.ToString("MM-dd");
                case EnumFormatMode.MM月dd日: return date.ToString("MM月dd日");
                case EnumFormatMode.yyyy_MM: return date.ToString("yyyy-MM");
                case EnumFormatMode.yyyy年MM月: return date.ToString("yyyy年MM月");
                case EnumFormatMode.yyyy_MM_dd_HH_mm_ss: return date.ToString("yyyy-MM-dd-HH-mm-ss");
                case EnumFormatMode.yyyy年MM月dd日HH时mm分ss秒: return date.ToString("yyyy年MM月dd日HH时mm分ss秒");
                default: return date.ToString();
            }
        }

        /// <summary>
        /// 转 yyyyMM 期间字符串
        /// </summary>
        public static string ToPeriod(this DateTime date)
        {
            return date.ToString("yyyyMM");
        }

        /// <summary>
        /// 转 yyyyMMdd 时间字符串
        /// </summary>
        public static string DateToPeriod(this DateTime date)
        { return date.ToString("yyyyMMdd"); }

        /// <summary>
        /// 生成完整时间code yyyyMMddHHmmss
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDateTimeCode(this DateTime date)
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        /// <summary>
        /// 上年首日
        /// </summary>
        public static DateTime LastYearFirstDay(this DateTime date)
        { return new DateTime(date.Year - 1, 1, 1); }

        /// <summary>
        /// 当年首日
        /// </summary>
        public static DateTime ThisYearFirstDay(this DateTime date)
        { return new DateTime(date.Year, 1, 1); }

        /// <summary>
        /// 下年首日
        /// </summary>
        public static DateTime NextYearFirstDay(this DateTime date)
        { return new DateTime(date.Year + 1, 1, 1); }

        /// <summary>
        /// 上月首日
        /// </summary>
        public static DateTime LastMonthFirstDay(this DateTime date)
        { return date.AddMonths(-1).AddDays(1 - date.Day); }

        /// <summary>
        /// 当月首日
        /// </summary>
        public static DateTime ThisMonthFirstDay(this DateTime date)
        { return date.AddDays(1 - date.Day); }

        /// <summary>
        /// 下月首日
        /// </summary>
        public static DateTime NextMonthFirstDay(this DateTime date)
        {
            return date.AddMonths(1).AddDays(1 - date.Day).Date;
        }

        /// <summary>
        /// 明天
        /// </summary>
        public static DateTime NextDayDate(this DateTime date)
        {
            return date.Date.AddDays(1);
        }

        /// <summary>
        /// 比较是否同年同月
        /// </summary>
        public static bool CompareMonth(this DateTime date, DateTime CompareDate)
        {
            if ((date.Year == CompareDate.Year) && (date.Month == CompareDate.Month))
            { return true; }
            return false;
        }
    }
}