using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassiveTest.Interface
{
    /// <summary>
    /// Describes abstract graph node
    /// </summary>
    public interface IGraphNode
    {
        /// <summary>
        /// Unique string identifying the node
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Human readable text
        /// </summary>
        string Label { get; set; }

        /// <summary>
        /// IDs of nodes adjacent to current
        /// </summary>
        string[] AdjacentNodes { get; }
    }
}
