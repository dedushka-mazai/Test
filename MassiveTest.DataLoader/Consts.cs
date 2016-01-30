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
        internal const int RC_SERVICE_CONNECT_ERROR = 3;

        internal static string[] ErrorMessages = new string[4]
        {
            "",                                         
            "Unexpected error.",                        
            "Specified nodes folder is not valid",      
            "Could not connect to the Graph Service."
        };

    }
}
