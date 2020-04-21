namespace AX.Core.CommonModel
{
    public class AXDataBaseException : AXCoreException
    {
        public string Sql { get; set; }

        /// <summary>
        /// 框架数据相关操作异常
        /// </summary>
        /// <param name="errMessage"></param>
        public AXDataBaseException(string errMessage) : base(errMessage)
        {
        }

        /// <summary>
        /// 框架数据相关操作异常
        /// </summary>
        /// <param name="errMessage"></param>
        public AXDataBaseException(string errMessage, string sql) : base(errMessage)
        {
            Sql = sql;
        }
    }
}