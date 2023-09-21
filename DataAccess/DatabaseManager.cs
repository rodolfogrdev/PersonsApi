
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{

    public class DatabaseManager: IDatabaseManager
    {        
        private readonly IConfiguration _configuration;
        private readonly ILogger<DatabaseManager> _logger;

        public DatabaseManager(IConfiguration configuration, ILogger<DatabaseManager> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        private SqlConnection GetConnection()
        {
            var connectionString =  _configuration.GetConnectionString("PointOfSaleDB");
            return new SqlConnection(connectionString);
        }

        public DataTable ExecuteQuery(string sql, List<SqlParameter>? sqlParams)
        {
            var dt = new DataTable();

            try
            {
                using (var connection = GetConnection())
                {
                    var command = new SqlCommand(sql, connection);
                    command.CommandTimeout = 30;
                    command.CommandType = CommandType.StoredProcedure;

                    if (sqlParams?.Count > 0) command.Parameters.AddRange(sqlParams.ToArray());

                    var dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = command;
                    dataAdapter.Fill(dt);
                }
            }
            catch (SqlException sqlExc)
            {
                _logger.LogError(sqlExc, "Database Error");
            }

            return dt;
        }

        public string ExecuteNonQuery(string sql, List<SqlParameter> sqlParams)
        {            
            if(sqlParams.Count <= 0)
            {
                throw new ArgumentNullException();
            }

            try
            {
                using (var connection = GetConnection())
                {
                    var command = new SqlCommand(sql, connection);
                    command.CommandTimeout = 30;
                    command.CommandType = CommandType.StoredProcedure;

                    if (sqlParams?.Count > 0) command.Parameters.AddRange(sqlParams.ToArray());
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (SqlException sqlExc)
            {
                _logger.LogError(sqlExc, "Database Error");
                return sqlExc.ToString();
            }
            return string.Empty;
        }
    }    
}

