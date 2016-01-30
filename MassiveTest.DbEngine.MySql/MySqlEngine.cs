using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using MassiveTest.Interface;
using MySql.Data.MySqlClient;

namespace MassiveTest.DataAccess.MySql
{
    /// <summary>
    /// MySql database engine
    /// </summary>
    public class MySqlEngine : IDbEngine
    {
        private MySqlConnection conn = null;

        /// <summary>
        /// Connects to the specified database
        /// </summary>
        /// <param name="connectionParams">Connection parameters (user name, password, host etc)</param>
        public void Connect(IDbConnectionParams connectionParams)
        {
            string connStr = String.Format("server={0};uid={1};pwd={2};", connectionParams.Host, connectionParams.User, connectionParams.Pass);
            conn = new MySqlConnection(connStr);
            conn.Open();
            conn.ChangeDatabase(connectionParams.DbName);
        }

        /// <summary>
        /// Disconnects from the database
        /// </summary>
        public void Disconnect()
        {
            if (conn != null)
                conn.Close();
        }

        /// <summary>
        /// Executes an SQL query, which returns a single value
        /// </summary>
        /// <param name="sql">SQL query text</param>
        /// <returns>Value</returns>
        public object ExecScalar(string sql)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// Executes SQL query, which returns dataset
        /// </summary>
        /// <param name="sql">SQL query text</param>
        /// <returns>Reader to access returned dataset</returns>
        public DbDataReader Execute(string sql)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                return cmd.ExecuteReader();
            }
        }

        /// <summary>
        /// Executes SQL script
        /// </summary>
        /// <param name="sql">SQL script text</param>
        /// <returns>Count of executed blocks in the script</returns>
        public int ExecScript(string sql)
        {
            var script = new MySqlScript(conn);
            script.Query = sql;
            return script.Execute();           
        }


    }
}
