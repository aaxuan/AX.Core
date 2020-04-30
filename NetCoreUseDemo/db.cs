﻿using MySql.Data.MySqlClient;
using System.Data.Common;

namespace NetCoreUseDemo
{
    public class DB : AX.Core.DataBase.AXDataBase
    {
        private DbConnection _connection;

        public override DbConnection Connection
        {
            get
            {
                if (_connection == null)
                { _connection = new MySqlConnection("server=localhost;userid=root;pwd=123qwe;database=test;sslmode=none;"); }

                return _connection;
            }
        }

        public override DbProviderFactory Factory { get { return new MySql.Data.MySqlClient.MySqlClientFactory(); } }
    }
}