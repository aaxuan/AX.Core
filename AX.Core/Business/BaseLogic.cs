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
    }
}