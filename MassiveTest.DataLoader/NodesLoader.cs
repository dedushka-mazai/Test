using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassiveTest.DataLoader.Tools;

namespace MassiveTest.DataLoader
{
    public class NodesLoader
    {
        private string nodesFolder;
        private List<GraphNode> nodes = new List<GraphNode>();
        private MassiveTest.Wcf.Client.DataManagement.DataManagementServiceClient proxy = null;
        private int lastResult = Consts.RC_OK;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodesFolder"></param>
        public NodesLoader(string nodesFolder)
        {
            this.nodesFolder = nodesFolder;
        }

        private void LoadNodesFromFolder()
        {
            Logger.WriteLine("Loading files...", true);
            nodes.Clear();

            foreach (var file in Directory.EnumerateFiles(nodesFolder))
            {
                try
                {
                    var node = XmlStorageProvider.LoadFromFile<GraphNode>(file);
                    node.SourceFile = file;
                    nodes.Add(node);
                }
                catch (Exception e)
                {
                    Logger.WriteLine("  WARNING. Invalid node file: " + file, true, true, false, ConsoleColor.DarkRed);
                    Logger.WriteException("", e);
                }
            }
        }

        private void ConnectToDataService()
        {
            Logger.Write("Connecting to Graph Service...", true, true);
            try
            {
                proxy = new MassiveTest.Wcf.Client.DataManagement.DataManagementServiceClient();
                proxy.Open();
                Logger.WriteLineSuccess("Ok", true, true, true);
            }
            catch (Exception e)
            {
                Logger.WriteLineError("Failed", true, true, true);
                Logger.WriteException("Could not connect to Graph Service", e);
                lastResult = Consts.RC_SERVICE_CONNECT_ERROR;
            }
        }

        private void DisconnectFromDataService()
        {
            proxy.Close();
        }
            
        private void UploadNodes()
        {
            Logger.WriteLine("Uploading nodes to Graph Service...", true);

            int errCount = 0;
            foreach (GraphNode node in nodes)
            {
                try
                {
                    Logger.Write(String.Format("  {0}...  ", Path.GetFileName(node.SourceFile)), true);
                    proxy.AddNode(node.Id, node.Label, node.AdjacentNodes);
                    Logger.WriteLineSuccess("Ok", true, true, true);
                }
                catch (Exception e)
                {
                    Logger.WriteLineError("Failed", true, true, true);
                    Logger.WriteException("Error while uploading node", e);
                    errCount++;
                }
            }

            Logger.WriteLine("", true);        
            Logger.WriteLine(String.Format("Upload completed ({0} errors)", errCount), true, true, false, ConsoleColor.White);
        }

        private void ClearNodes()
        {
            proxy.Clear();
        }

        public int Execute()
        {
            LoadNodesFromFolder();
            if (nodes.Count == 0)
            {
                Logger.WriteLine("No graph nodes found.", true, true, false, ConsoleColor.White);
                return Consts.RC_OK;
            }

            ConnectToDataService();
            if (lastResult != Consts.RC_OK)
                return lastResult;

            ClearNodes();

            UploadNodes();

            DisconnectFromDataService();

            return Consts.RC_OK;
        }
    }
}
