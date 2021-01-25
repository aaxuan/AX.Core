using System;
using System.Linq;
using System.Text;

namespace AX
{
    public static partial class Extention
    {
        private static string GetExceptionLine(Exception exception)
        {
            StringBuilder result = new StringBuilder();
            exception?.StackTrace?.Split("\r\n".ToArray())?.ToList()?.ForEach(item =>
            {
                if (item.Contains("行号") || item.Contains("line"))
                { result.Append($"    {item}\r\n"); }
            });
            return string.IsNullOrEmpty(result.ToString()) ? " 获取异常行号为空 " : result.ToString();
        }

        /// <summary>
        /// 递归获取全部内部异常
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private static string GetAllExceptionMsg(Exception ex, int level = 1)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($@"{level}层异常: {ex?.Message} 位置: {GetExceptionLine(ex)}");
            if (ex.InnerException != null)
            {
                builder.Append(GetAllExceptionMsg(ex.InnerException, level + 1));
            }
            return builder.ToString();
        } 
    }
}