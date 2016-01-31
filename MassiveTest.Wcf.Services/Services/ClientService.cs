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
    /// Describes operations for client front-end applications
    /// </summary>
    [ServiceContract]
    public interface IClientService
    {
        /// <summary>
        /// Gets graph
        /// </summary>
        /// <returns>Array of graph nodes</returns>
        [OperationContract]
        NodeInfo[] GetGraph();
    }
    #endregion

    #region Service implementation
    /// <summary>
    /// Client front-end oriented service
    /// </summary>
    public class ClientService : IClientService
    {
        private IDataProvider provider;

        /// <summary>
        /// Initializes a new instance of the client service
        /// </summary>
        /// <param name="provider">Service data provider</param>
        public ClientService(IDataProvider provider)
        {
            this.provider = provider;
        }

        /// <summary>
        /// Gets graph from the data store
        /// </summary>
        /// <returns>Array of graph nodes</returns>
        public NodeInfo[] GetGraph()
        {
            return null;
        }
    }
    #endregion
}
