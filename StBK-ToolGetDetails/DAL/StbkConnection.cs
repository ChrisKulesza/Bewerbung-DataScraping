using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace StBK_ToolGetDetails.DAL
{
    public class StbkConnection : IDisposable, IAsyncDisposable
    {
        private static readonly string DefaultConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

        private SqlConnection _sqlConnection;

        public static async Task<StbkConnection> CreateAsync(string? connectionString = null)
        {
            connectionString ??= DefaultConnectionString;

            var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            return new StbkConnection(connection);
        }

        private StbkConnection(SqlConnection connection)
        {
            _sqlConnection = connection;
        }

        /// <summary>
        /// Get the connection object
        /// </summary>
        /// <param name="text">The query or Stored procedure to execute</param>
        /// <param name="type">Define if it's a query or a stored procedure</param>
        /// <returns>The connection</returns>
        private SqlCommand CreateSqlCommand(string query, CommandType type)
        {
            var cmd = new SqlCommand(query, _sqlConnection);
            cmd.CommandType = type;
            return cmd;
        }

        /// <summary>
        /// Get the result of the SELECT statements in a DataSet
        /// </summary>
        /// <param name="cmd">The command to open</param>
        /// <returns>The DataSet with the SELECT statements</returns>
        public async IAsyncEnumerable<IDataRecord> GetDataSet(string query, Dictionary<string, object> parameters = null)
        {
            using var cmd = CreateSqlCommand(query, CommandType.Text);

            if (parameters is not null)
                cmd.AddParameters(parameters);

            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                yield return reader;
            }

            // Logging connection state
            //Console.WriteLine(cmd.Connection.State);
            //using var adapter = new SqlDataAdapter(cmd);
            //adapter.Fill(ds);

            // return ds;
        }

        /// <summary>
        /// Execute the query with parameters
        /// </summary>
        /// <param name="query">Query to execute</param>
        /// <param name="parameters">The stored procedure's params, the dictionary's Key and the stored procedure's parameter must have the same name</param>
        /// <returns>True if one or more rows were affected, other way False </returns>
        public bool ExecuteNonQueryFromQuery(string query, Dictionary<string, object> parameters = null)
        {
            using var cmd = CreateSqlCommand(query, CommandType.Text);
            if (parameters is not null) cmd.AddParameters(parameters);

            return ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="cmd">The command to open.</param>
        /// <returns>True if one or more rows were affected, other way False.</returns>
        private bool ExecuteNonQuery(SqlCommand cmd)
        {
            try
            {
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void Dispose()
        {
            _sqlConnection.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await _sqlConnection.DisposeAsync();
        }
    }
}