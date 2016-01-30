using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassiveTest.DataLoader
{
    class Consts
    {
        internal const int RC_OK = 0;
        internal const int RC_UNEXPECTED_ERROR = 1;
        internal const int RC_INVALID_NODES_FOLDER = 2;

        internal static string[] ErrorMessages = new string[3]
        {
            "Success",                                         // 0
            "Unexpected error.",                               // 2
            "Specified nodes folder is not valid"/*,           // 3
            "MySQL user name is not specified.",               // 4
            "MySQL server is not specified.",                  // 5
            "MySQL user password is not specified.",           // 6
            "MySQL database is not specified.",                // 7
            "Could not connect to MySQL server.",              // 8
            "Database error.",                                 // 9
            "Present database is not valid.",                  // 10
            "Newer version of database is already installed.", // 11
            "Database does not exist."                         // 12*/
        };

    }
}
