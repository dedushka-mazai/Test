using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace MassiveTest.Interface
{
    /// <summary>
    /// Describes database engine, which can be used to access abstract database
    /// </summary>
    public interface IDbEngine
    {
        /// <summary>
        /// Connects to the database
        /// </summary>
        /// <param name="connectionParams">Connection parameters (user name, password, host etc)</param>
        void Connect(IDbConnectionParams connectionParams);

        /// <summary>
        /// Disconnects from the database
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Executes an SQL query, which returns a single value
        /// </summary>
        /// <param name="sql">SQL query text</param>
        /// <returns>Value</returns>
        object ExecScalar(string sql);

        /// <summary>
        /// Executes SQL query, which returns dataset
        /// </summary>
        /// <param name="sql">SQL query text</param>
        /// <returns>Reader to access returned dataset</returns>
        DbDataReader Execute(string sql);

        /// <summary>
        /// Executes SQL script
        /// </summary>
        /// <param name="sql">SQL script text</param>
        /// <returns>Count of executed blocks in a script</returns>
        int ExecScript(string sql);

    }
}
