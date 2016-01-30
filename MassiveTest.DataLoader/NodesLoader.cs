using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassiveTest.DataLoader.Tools;

namespace MassiveTest.DataLoader
{
    /// <summary>
    /// Class, that allows load graph nodes from the disk and then upload its to the Graph Service
    /// </summary>
    public class NodesLoader
    {
        #region Private members

        // nodes folder name
        private string nodesFolder;
        // nodes list
        private List<GraphNode> nodes = new List<GraphNode>();
        // proxy to call Graph Service methods
        private MassiveTest.Wcf.Client.DataManagement.DataManagementServiceClient proxy = null;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes instance of the class, which loads graph nodes from the specified folder on the disk and uploads its to the Graph Service
        /// </summary>
        /// <param name="nodesFolder">Folder name, where graph nodes are located</param>
        public NodesLoader(string nodesFolder)
        {
            this.nodesFolder = nodesFolder;
        }
        #endregion

        #region Private methods
        // loads nodes from the specified folder into the internal list
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

        // connects to the Graph Service
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
                throw new DataLoaderException(OperationResult.ServiceConnectError);
            }
        }

        // disconnects from the Graph Service
        private void DisconnectFromDataService()
        {
            try
            {
                proxy.Close();
            }
            catch (Exception e)
            {
                Logger.WriteException("Error while disconnecting from Graph Service", e);
                throw new DataLoaderException(OperationResult.ServiceDisconnectError);
            }
        }
            
        // uploads nodes to the Graph Service
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

        // clears nodes at the Graph Service
        private void ClearNodes()
        {
            try
            {
                proxy.Clear();
            }
            catch (Exception e)
            {
                Logger.WriteException("Error while clearing nodes", e);
                throw new DataLoaderException(OperationResult.ServiceClearNodesError);
            }
        }
        #endregion

        #region Main execution procedure
        /// <summary>
        /// Loads nodes from the specified folder and uploads them to the Graph Service
        /// </summary>
        public void Execute()
        {
            // load nodes from disk into the list
            LoadNodesFromFolder();

            // if nodes list is empty then stop processing and quit with rc = 0
            if (nodes.Count == 0)
            {
                Logger.WriteLine("No graph nodes found.", true, true, false, ConsoleColor.White);
                return; 
            }

            // connect to WCF graph data-management service
            ConnectToDataService();

            // delete all existing nodes at the service
            ClearNodes();

            // upload previously loaded from disk nodes to the service
            UploadNodes();

            // disconnect from the service
            DisconnectFromDataService();
        }
        #endregion
    }
}
