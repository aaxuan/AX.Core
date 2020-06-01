using AX.Core.Business.DataModel;
using AX.Core.DataBase;

namespace AX.Core.Business
{
    public class BaseLogic
    {
        public static DataRepository DB { get { return GetStaticDB(); } }

        public static Base_User CurrentUser { get { return Managers.AuthLogic.GetCurrentUser(); } }

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