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
        static string nodesFolder = null;

        static void Main(string[] args)
        {
            Logger.WriteLine("MassiveTest Data Loader 1.0, Copyright (c) Sergey Stoyan, 2016\n", true);
            try
            {
                if (args.Length == 0)
                {
                    ShowHelp();
                    Terminate(Consts.RC_OK);
                }

                ParseArgs(args);
               
                var proxy = new MassiveTest.Wcf.Client.DataManagement.DataManagementServiceClient();
                Console.WriteLine(proxy.Clear().ToString());
                proxy.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Logger.WriteException("Unexpected error. ", e);
                Terminate(Consts.RC_UNEXPECTED_ERROR);
            }
        }

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
                Terminate(Consts.RC_INVALID_NODES_FOLDER);
            }
        }

        /// <summary>
        /// Terminates application returning specified result code
        /// </summary>
        /// <param name="resultCode">Result code</param>
        static void Terminate(int resultCode)
        {
            // write message to console if error occured
            if (resultCode != Consts.RC_OK)
            {
                Logger.WriteLineError(Consts.ErrorMessages[resultCode], true);
            }
            // quit application
            Environment.Exit(resultCode);
        }
    }
}
