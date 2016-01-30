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
        /// Fills graph with set of nodes
        /// </summary>
        /// <param name="graph">Graph instance to create nodes in</param>
        void BuildGraph(IGraph graph);
    }
}
