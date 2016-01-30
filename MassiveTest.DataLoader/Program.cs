using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassiveTest.Interface;
using System.IO;
using System.Reflection;

namespace MassiveTest.DataLoader
{
    class Program
    {
        // folder, where nodes' files are located
        static string nodesFolder = null;

        /// <summary>
        /// Application entry point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Logger.WriteLine("MassiveTest Data Loader 1.0, Copyright (c) Sergey Stoyan, 2016\n", true);
            try
            {
                // if no arguments specified, show help and quit
                if (args.Length == 0)
                {
                    ShowHelp();
                    Terminate(OperationResult.Ok);
                }

                // arguments parsing
                ParseArgs(args);

                // create loader instance
                var loader = new NodesLoader(nodesFolder);

                // execute loader procedure 
                loader.Execute();

                // quit with result code = 0
                Terminate(OperationResult.Ok);
            }
            catch (DataLoaderException e)
            {
                Terminate(e.ResultCode);
            }
            catch (Exception e)
            {
                Logger.WriteException("Unexpected error. ", e);
                Terminate(OperationResult.UnexpectedError);
            }
        }

        /// <summary>
        /// Displays help information
        /// </summary>
        static void ShowHelp()
        {
            Logger.WriteLine("Usage:  MassiveTest.DataLoader [folder] ", true);
            Logger.WriteLine("", true);
            Logger.WriteLine("    folder - input folder where nodes' xml-files are located", true);
        }

        /// <summary>
        /// Parses command line arguments
        /// </summary>
        /// <param name="args">Array of arguments</param>
        static void ParseArgs(string[] args)
        {
            nodesFolder = args[0];
            if (!Path.IsPathRooted(nodesFolder))
                nodesFolder = Path.GetFullPath(nodesFolder);

            // exit if nodes folder is not valid
            if (!Directory.Exists(nodesFolder))
            {
                Terminate(OperationResult.InvalidNodesFolder);
            }
        }

        /// <summary>
        /// Terminates application returning specified result code
        /// </summary>
        /// <param name="resultCode">Result code</param>
        static void Terminate(OperationResult resultCode)
        {
            // write message to console if error occured
            if (resultCode != OperationResult.Ok)
            {
                Logger.WriteLineError(Consts.ErrorMessages[(byte)resultCode], true);
            }
            // quit application
            Environment.Exit((byte)resultCode);
        }
    }
}
