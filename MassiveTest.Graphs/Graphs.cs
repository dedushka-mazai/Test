using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassiveTest.Interface;

namespace MassiveTest.Graphs
{
    /// <summary>
    /// Class, implements unidirectional graph
    /// </summary>
    public class Graph : IGraph
    {
        #region Private
        
        private Dictionary<string, Node> nodes = new Dictionary<string, Node>();

        // Performs backtrace from the end node to the start one
        private string[] Backtrace(Node startNode, Node endNode)
        {
            var path = new string[endNode.waveMark + 1];
            var node = endNode;
            do
            {
                path[node.waveMark] = node.Id;
                if (node != startNode)
                {
                    foreach (var btNode in node.backtraceNodes.Values)
                    {
                        if (btNode.waveMark + 1 == node.waveMark)
                        {
                            node = btNode;
                            break;
                        }
                    }
                }
                else
                    node = null;
            }
            while (node != null);

            return path;
        }

        // Checks string argument for null-value or empty-value 
        private void CheckArgumentStringForNullOrEmpty(string value, string argumentName)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentNullException(argumentName, "Value can not be null or empty");
        }

        // Checks if specified node id belongs to the graph
        private void CheckArgumentNodeBelongsToGraph(string nodeId, string argumentName)
        {
            if (!nodes.ContainsKey(nodeId))
                throw new ArgumentException(String.Format("Node (id = '{0}') does not belong to the graph", nodeId), argumentName);
        }

        // resets wavemarks for all the nodes in the graph
        private void ResetAllNodesWaveMarks()
        {
            foreach (var node in nodes.Values)
                node.waveMark = -1;
        }

        #endregion

        #region IGraph members
        /// <summary>
        /// Adds node to the graph.
        /// </summary>
        /// <param name="id">A unique string identifying the node</param>
        /// <param name="label">Human readable text to be displayed in UI</param>
        /// <param name="adjacentIds">IDs of nodes adjacent to current</param>
        public void AddNode(string id, string label, string[] adjacentIds)
        {
            // check arguments
            CheckArgumentStringForNullOrEmpty(id, "id");
            CheckArgumentStringForNullOrEmpty(label, "label");

            // create node and add it to the graph's node list
            Node node = new Node() { Id = id, Label = label };
            nodes.Add(node.Id, node);

            // add adjacent nodes
            if (adjacentIds != null)
            {
                foreach (string adjId in adjacentIds)
                {
                    // if adjacent node already present, add its instance 
                    // otherwise add null-value (should be updated when node instance will be created)
                    Node adjNode = null;
                    if (nodes.ContainsKey(adjId))
                        adjNode = nodes[adjId];
                    node.adjacentNodes.Add(adjId, adjNode);

                    // update adjacent node backtrace node list
                    if (adjNode != null)
                        adjNode.backtraceNodes.Add(node.Id, node);
                }
            }

            // update adjacent lists for all nodes with the node instance
            foreach (var n in nodes.Values)
            {
                if (n.adjacentNodes.ContainsKey(node.Id))
                {
                    n.adjacentNodes[node.Id] = node;
                    // update node backtrace list
                    if (!node.backtraceNodes.ContainsKey(n.Id))
                        node.backtraceNodes.Add(n.Id, n);
                }
            }
        }

        /// <summary>
        /// Clears the graph
        /// </summary>
        public void Clear()
        {
            nodes.Clear();
        }

        /// <summary>
        /// Finds shortest path between two specified nodes
        /// </summary>
        /// <param name="startId">Id of start node</param>
        /// <param name="endId">Id of end node</param>
        /// <returns>Array of IDs (from start to end) if path exists, or empty array otherwise</returns>
        public string[] FindShortestPath(string startId, string endId)
        {
            // check arguments
            CheckArgumentStringForNullOrEmpty(startId, "startId");
            CheckArgumentStringForNullOrEmpty(endId, "endId");

            CheckArgumentNodeBelongsToGraph(startId, "startId");
            CheckArgumentNodeBelongsToGraph(endId, "endId");

            // reset wave marks for all nodes
            ResetAllNodesWaveMarks();

            // init start data
            int i = -1;
            bool found = false;
            nodes[startId].waveMark = 0;
            var oldWave = new List<Node>();
            var newWave = new List<Node>();
            newWave.Add(nodes[startId]);

            // main loop
            while (!found && (newWave.Count > 0))
            {
                // prepare waves
                oldWave.Clear();
                oldWave.AddRange(newWave);
                newWave.Clear();
                i++;

                // wave expansion
                foreach (var node in oldWave)
                {
                    foreach (var adjacentNode in node.adjacentNodes.Values)
                    {
                        if (adjacentNode != null && adjacentNode.waveMark == -1)
                        {
                            adjacentNode.waveMark = i + 1;
                            newWave.Add(adjacentNode);
                            if (adjacentNode == nodes[endId])
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                    if (found) break;
                }
            }

            // return backtrace if path found or empty path otherwise
            return found ? Backtrace(nodes[startId], nodes[endId]) : new string[0];
        }
        #endregion
    }

    /// <summary>
    /// Node class for internal usage
    /// </summary>
    internal class Node
    {
        internal int waveMark = -1;
        internal Dictionary<string, Node> backtraceNodes = new Dictionary<string, Node>();
        internal Dictionary<string, Node> adjacentNodes = new Dictionary<string, Node>();

        internal string Id { get; set; }
        internal string Label { get; set; }
    }
}
