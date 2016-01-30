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
    /// Describes data management operations
    /// </summary>
    [ServiceContract]
    public interface IDataManagementService
    {
        /// <summary>
        /// Adds node to the data store
        /// </summary>
        /// <param name="id">Unique string identifier</param>
        /// <param name="label">Human readable text</param>
        /// <param name="adjacentIds">List of adjacent nodes' Ids</param>
        /// <returns>Result Code</returns>
        [OperationContract]
        DataOperationResult AddNode(string id, string label, string[] adjacentIds);

        /// <summary>
        /// Erases all the nodes in the data store
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataOperationResult Clear();
    }

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
}
