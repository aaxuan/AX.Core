using System.Data.Common;

namespace AX
{
    public static partial class Extention
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