using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MassiveTest.Interface;

namespace MassiveTest.Wcf.Services
{
    /// <summary>
    /// Domain-specific service implementation
    /// </summary>
    public class DomainSpecificService : IDomainSpecificService
    {
        private IGraph graph;
        private IDataProvider provider;

        /// <summary>
        /// Initializes a new instance of domain specific service with specified DataProvider and Graph parameters
        /// </summary>
        /// <param name="provider">Data Provider instance</param>
        /// <param name="graph">Graph instance</param>
        public DomainSpecificService(IDataProvider provider, IGraph graph)
        {
            this.graph = graph;
            this.provider = provider;
        }

        /// <summary>
        /// Calculates shortest path between two specified nodes
        /// </summary>
        /// <param name="startId">Start node Id</param>
        /// <param name="endId">End node Id</param>
        /// <returns>Shortest path if exists, or empty array otherwise</returns>
        public string[] GetShortestPath(string startId, string endId)
        {
            provider.BuildGraph(graph);
            return graph.FindShortestPath(startId, endId);
        }
    }
}
