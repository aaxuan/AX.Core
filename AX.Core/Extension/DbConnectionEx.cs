using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace AX.Core.Extension
{
    public static class DbConnectionEx
    {
        public static void TryOpen(this DbConnection dbConnection)
        {
            if (dbConnection.State != System.Data.ConnectionState.Open)
            {
                dbConnection.Open();
            }
        }
    }
}
