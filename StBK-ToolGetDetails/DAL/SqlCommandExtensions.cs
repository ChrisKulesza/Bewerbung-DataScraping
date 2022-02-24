using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace StBK_ToolGetDetails.DAL
{
    /// <summary>
    /// Data Access Layer Class to connect with SQL Server
    /// </summary>
    public static class SqlCommandExtensions
    {
        /// <summary>
        /// Add parameters to a command
        /// </summary>
        /// <param name="cmd">command</param>
        /// <param name="parameters">The stored procedure's params, the dictionary's Key and the stored procedure's parameter must have the same name</param>
        /// <returns>The command with the parameters added</returns>
        public static SqlCommand AddParameters(this SqlCommand cmd, Dictionary<string, object> parameters)
        {
            foreach (var parameter in parameters)
                cmd.Parameters.AddWithValue($"@{parameter.Key}", parameter.Value);

            return cmd;
        }
    }
}