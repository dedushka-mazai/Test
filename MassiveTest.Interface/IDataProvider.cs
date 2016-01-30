using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassiveTest.Interface
{
    /// <summary>
    /// Represents class, which provide data for the graph
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Fills specified graph with set of nodes
        /// </summary>
        /// <param name="graph">Graph instance to create nodes in</param>
        void BuildGraph(IGraph graph);

        /// <summary>
        /// Deletes all the nodes in data store
        /// </summary>
        void ClearNodes();

        /// <summary>
        /// Adds new node to the data store
        /// </summary>
        /// <param name="id">Node id</param>
        /// <param name="label">Node label</param>
        /// <param name="adjacentNodes">Adjacent node list</param>
        void AddNode(string id, string label, string[] adjacentNodes);
    }
}
