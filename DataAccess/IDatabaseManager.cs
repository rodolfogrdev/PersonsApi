using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public interface IDatabaseManager
    {
        DataTable ExecuteQuery(string sql, List<SqlParameter> sqlParams);

        string ExecuteNonQuery(string sql, List<SqlParameter> sqlParams);
    }
}
