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
    /// Represents information about graph node
    /// </summary>
    [DataContract]
    public class NodeInfo
    {
        /// <summary>
        /// Unique string identifying the node
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Human readable text to be displayed in UI
        /// </summary>
        [DataMember]
        public string Label { get; set; }

        /// <summary>
        /// IDs of nodes adjacent to current
        /// </summary>
        [DataMember]
        public string[] AdjacentNodes { get; set; }
    }
}