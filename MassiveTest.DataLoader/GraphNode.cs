using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MassiveTest.DataLoader
{
    [XmlRoot("node")]
    [Serializable]
    public class GraphNode
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("label")]
        public string Label { get; set; }

        [XmlArray("adjacentNodes")]
        [XmlArrayItem("id")]
        public string[] AdjacentNodes { get; set; }

        [XmlIgnore]
        public string SourceFile { get; set; }
    }
}
