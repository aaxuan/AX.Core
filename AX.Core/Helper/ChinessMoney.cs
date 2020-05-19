using System.Text;

namespace AX.Core.Helper
{
    /// <summary>
    ///
    /// </summary>
    public static class ChinessMoney
    {
        private readonly static string[] _ChineseNums = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
        private readonly static string[] _Units = { "分", "角", "元", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟" };

        /// <summary>
        /// 转中文大写数字金钱数
        /// </summary>
        public static string ToChineseMoney(decimal value)
        {
            if (value <= 0)
            { return "零元整"; }

            StringBuilder strValue = new StringBuilder();
            string strNum = decimal.Truncate(value * 100).ToString();
            int len = strNum.Length;
            int zero = 0;
            for (int i = 0; i < len; i++)
            {
                int num = int.Parse(strNum.Substring(i, 1));
                int unitNum = len - i - 1;
                if (num == 0)
                {
                    zero++;
                    if (unitNum == 2 || unitNum == 6 || unitNum == 10)
                    {
                        if (unitNum == 2 || zero < 4)
                            strValue.Append(_Units[unitNum]);
                        zero = 0;
                    }
                }
                else
                {
                    if (zero > 0)
                    {
                        strValue.Append(_Units[0]);
                        zero = 0;
                    }
                    strValue.Append(_Units[num]);
                    strValue.Append(_Units[unitNum]);
                }
            }
            if (zero > 0)
            { strValue.Append("整"); }
            return strValue.ToString();
        }
    }
}