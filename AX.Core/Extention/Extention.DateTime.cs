using System;
using System.Globalization;

namespace AX
{
    public static partial class Extention
    {
        ///   <summary>
        ///  获取某一日期是该年中的第几周
        ///   </summary>
        ///   <param name="dateTime"> 日期 </param>
        ///   <returns> 该日期在该年中的周数 </returns>
        public static int GetWeekOfYear(this DateTime dateTime)
        {
            GregorianCalendar gregorianCalendar = new GregorianCalendar();
            return gregorianCalendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        ///// <summary>
        ///// 获取Js格式的timestamp
        ///// </summary>
        ///// <param name="dateTime">日期</param>
        ///// <returns></returns>
        //public static long ToJsTimestamp(this DateTime dateTime)
        //{
        //    var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
        //    long result = (dateTime.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位
        //    return result;
        //}

        /// <summary>
        /// 获取js中的getTime()
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns></returns>
        public static Int64 JsGetTime(this DateTime dt)
        {
            Int64 retval = 0;
            var st = new DateTime(1970, 1, 1);
            TimeSpan t = (dt.ToUniversalTime() - st);
            retval = (Int64)(t.TotalMilliseconds + 0.5);
            return retval;
        }
    }
}