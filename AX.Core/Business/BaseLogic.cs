using AX.Core.DataBase;

namespace AX.Core.Business
{
    public class BaseLogic
    {
        public DataRepository GetDB()
        {
            return GetStaticDB();
        }

        public static DataRepository GetStaticDB()
        {
            return DBFactory.GetDataRepository(DBFactory.DefaultDBKey);
        }

        public static void Log(string content)
        {
            Managers.SystemLogLogic.Log(content);
        }

        public static void Log(string type, string content)
        {
            Managers.SystemLogLogic.Log(type, content);
        }
    }
}