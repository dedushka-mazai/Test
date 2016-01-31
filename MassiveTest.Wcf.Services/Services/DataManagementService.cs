using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using MassiveTest.Interface;

namespace MassiveTest.Wcf.Services
{
    #region Service Contract
    /// <summary>
    /// Describes data store operations
    /// </summary>
    [ServiceContract]
    public interface IDataManagementService
    {
        /// <summary>
        /// Adds graph node to the data store
        /// </summary>
        /// <param name="nodeInfo">Graph node information</param>
        [OperationContract]
        void AddNode(NodeInfo nodeInfo);

        /// <summary>
        /// Erases all the graph nodes in the data store
        /// </summary>
        [OperationContract]
        void ClearNodes();
    }
    #endregion

    #region Service Implementation
    /// <summary>
    /// Data management service
    /// </summary>
    public class DataManagementService : IDataManagementService
    {
        private IDataProvider provider;

        /// <summary>
        /// Initializes a new instance of the data management service
        /// </summary>
        /// <param name="provider">Entity, that provides access to the data store</param>
        public DataManagementService(IDataProvider provider)
        {
            this.provider = provider;
        }

        /// <summary>
        /// Adds new node to the data store
        /// </summary>
        /// <param name="nodeInfo">Information about node to be added</param>
        public void AddNode(NodeInfo nodeInfo)
        {
            provider.AddNode(nodeInfo.Id, nodeInfo.Label, nodeInfo.AdjacentNodes);
        }

        /// <summary>
        /// Erases all the graph nodes at the data store
        /// </summary>
        public void ClearNodes()
        {
            provider.ClearNodes();
        }
    }
    #endregion
}
