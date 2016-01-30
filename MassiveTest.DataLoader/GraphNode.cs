using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MassiveTest.DataLoader
{
    /// <summary>
    /// Class for graph node xml deserialization
    /// </summary>
    [XmlRoot("node")]
    [Serializable]
    public class GraphNode
    {
        /// <summary>
        /// Node Id
        /// </summary>
        [XmlElement("id")]
        public string Id { get; set; }

        /// <summary>
        /// Node label
        /// </summary>
        [XmlElement("label")]
        public string Label { get; set; }

        /// <summary>
        /// List of adjacent nodes' ids
        /// </summary>
        [XmlArray("adjacentNodes")]
        [XmlArrayItem("id")]
        public string[] AdjacentNodes { get; set; }

        /// <summary>
        /// File name, where graph node load from 
        /// </summary>
        [XmlIgnore]
        public string SourceFile { get; set; }
    }
}
