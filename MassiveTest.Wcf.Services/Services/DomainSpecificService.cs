using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MassiveTest.Interface;

namespace MassiveTest.Wcf.Services
{
    #region Service Contract
    /// <summary>
    /// Describes domain-specific operations
    /// </summary>
    [ServiceContract]
    public interface IDomainSpecificService
    {
        /// <summary>
        /// Gets shortest path between two nodes
        /// </summary>
        /// <param name="startId">Start node Id</param>
        /// <param name="endId">End node Id</param>
        /// <returns>Path including start and end nodes if exists, or empty array otherwise</returns>
        [OperationContract]
        string[] GetShortestPath(string startId, string endId);
    }
    #endregion

    #region Service Implementation
    /// <summary>
    /// Domain-specific service
    /// </summary>
    public class DomainSpecificService : IDomainSpecificService
    {
        private IGraph graph;
        private IDataProvider provider;

        /// <summary>
        /// Initializes a new instance of domain specific service
        /// </summary>
        /// <param name="provider">Service data provider</param>
        /// <param name="graph">Entity, that implements graph logic</param>
        public DomainSpecificService(IDataProvider provider, IGraph graph)
        {
            this.graph = graph;
            this.provider = provider;
        }

        /// <summary>
        /// Calculates shortest path between two specified graph nodes
        /// </summary>
        /// <param name="startId">Start node Id</param>
        /// <param name="endId">End node Id</param>
        /// <returns>Shortest path (including start and end nodes) if exists, or empty array otherwise</returns>
        public string[] GetShortestPath(string startId, string endId)
        {
            // fill graph instance with data
            provider.BuildGraph(graph);
            
            // search for the shortest path between specified nodes
            return graph.FindShortestPath(startId, endId);
        }
    }
    #endregion
}
