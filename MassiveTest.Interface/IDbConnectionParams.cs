using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassiveTest.Interface
{
    /// <summary>
    /// Describes database connection parameters
    /// </summary>
    public interface IDbConnectionParams
    {
        /// <summary>
        /// Host name
        /// </summary>
        string Host { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        string User { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        string Pass { get; set; }

        /// <summary>
        /// Database name
        /// </summary>
        string DbName { get; set; }
    }
}
