using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassiveTest.Interface
{
    /// <summary>
    /// Describes an abstract graph
    /// </summary>
    public interface IGraph
    {
        /// <summary>
        /// Adds node to the graph
        /// </summary>
        /// <param name="id">String node identifier</param>
        /// <param name="label">Some human readable text, describing the node</param>
        /// <param name="adjacentIds">Array of adjacent nodes</param>
        void AddNode(string id, string label, string[] adjacentIds);

        /// <summary>
        /// Removes all nodes from the graph
        /// </summary>
        void Clear();

        /// <summary>
        /// Searches for the shortest path between two graph nodes
        /// </summary>
        /// <param name="startId">Start node Id</param>
        /// <param name="endId">End node Id</param>
        /// <returns>Ordered array of node's ids if path exists, or empty array otherwise</returns>
        string[] FindShortestPath(string startId, string endId);
    }
}
