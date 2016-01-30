using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassiveTest.DataLoader
{
    /// <summary>
    /// Operation results
    /// </summary>
    public enum OperationResult : byte
    {
        Ok                      = 0,
        UnexpectedError         = 1,
        InvalidNodesFolder      = 2,
        ServiceConnectError     = 3,
        ServiceDisconnectError  = 4,
        ServiceClearNodesError  = 5
    }

    /// <summary>
    /// Contains error messages' texts
    /// </summary>
    class Consts
    {
        internal static string[] ErrorMessages = new string[6]
        {
            "",                                         
            "Unexpected error.",                        
            "Specified nodes folder is not valid.",
            "Could not connect to the Graph Service.",
            "Error occured while disconnecting from the Graph Service.",
            "Error occured while clearing nodes"
        };
    }

    /// <summary>
    /// Data Loader exception class
    /// </summary>
    public class DataLoaderException : Exception
    {
        /// <summary>
        /// Data Loader operation result
        /// </summary>
        public OperationResult ResultCode { get; set; }

        /// <summary>
        /// Initializes Data Loader exception instance
        /// </summary>
        /// <param name="resultCode">Operation Result</param>
        public DataLoaderException(OperationResult resultCode)
            : base()
        {
            ResultCode = resultCode;
        }
    }
}
