namespace AX.Core.CommonModel.Exceptions
{
    public class AXDataBaseException : AXCoreException
    {
        public string Sql { get; set; }

        public AXDataBaseException(string errMessage) : base(errMessage)
        {
        }

        public AXDataBaseException(string errMessage, string sql) : base(errMessage)
        {
            Sql = sql;
        }
    }
}