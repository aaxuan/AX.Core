using AX.Core.Cache;
using System;

namespace AX.Core.DataBase
{
    public static class DBFactory
    {
        public enum DataBaseType
        {
            None = 0,
            MySql = 1,
            SqlLite = 2,
        }

        public static readonly string DefaultDBKey = "BASE";

        public static Func<Schema.SchemaDB, DataRepository> GetDataRepositoryFunc { get; set; }

        private static readonly MemoryCache<Schema.SchemaDB> _schemlDBCache;

        static DBFactory()
        {
            _schemlDBCache = CacheFactory.CreateCache<Schema.SchemaDB>("全局数据库链接缓存") as MemoryCache<Schema.SchemaDB>;
        }

        public static bool SetDB(string codeName, string connectionString, DataBaseType dataBaseType)
        {
            var result = false;
            var db = new Schema.SchemaDB();
            db.CodeName = codeName;
            db.ConnectionString = connectionString;
            db.DBType = dataBaseType;
            result = _schemlDBCache.Add(db.CodeName, db);
            return result;
        }

        public static DataRepository GetDataRepository(string DBname)
        {
            var dbScheml = _schemlDBCache.Get(DBname);
            var result = GetDataRepositoryFunc(dbScheml);
            result.UseConfig(dbScheml.DBType);
            return result;
        }
    }
}