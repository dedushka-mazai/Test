using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassiveTest.Interface;

namespace MassiveTest.DataProvider
{
    /// <summary>
    /// Data Provider, which gets\stores graph data from\into abstract database with known structure
    /// </summary>
    public class DbDataProvider : IDataProvider
    {
        private IDbEngine db;
        private IDbConnectionParams connectionParams;

        public DbDataProvider(IDbEngine db, IDbConnectionParams connectionParams)
        {
            this.db = db;
            this.connectionParams = connectionParams;
        }

        /// <summary>
        /// Builds specified graph with set of nodes stored in the database
        /// </summary>
        /// <param name="graph">Graph to fill</param>
        public void BuildGraph(IGraph graph)
        {
            graph.Clear();

            db.Connect(connectionParams);
            try
            {
                var reader = db.Execute("CALL get_graph();");
                try
                {
                    string id = null;
                    string label = null;
                    var adjacentNodes = new List<string>();

                    // read nodes from db and add its into the graph
                    while (reader.Read())
                    {
                        if ((reader.GetString(0) != id) && (id != null))
                        {
                            graph.AddNode(id, label, adjacentNodes.ToArray());
                            adjacentNodes.Clear();
                        }
                        id = reader.GetString(0);
                        label = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            adjacentNodes.Add(reader.GetString(2));
                    }

                    // add last node if exists
                    if (id != null)
                        graph.AddNode(id, label, adjacentNodes.ToArray());
                }
                finally
                {
                    reader.Close();
                }
            }
            finally
            {
                db.Disconnect();
            }
        }

        /// <summary>
        /// Deletes all the nodes in the database
        /// </summary>
        public void ClearNodes()
        {
            db.Connect(connectionParams);
            try
            {
                db.ExecScript("CALL clear();");
            }
            finally
            {
                db.Disconnect();
            }
        }

        /// <summary>
        /// Adds new node to the database
        /// </summary>
        /// <param name="id">Node id</param>
        /// <param name="label">Node label</param>
        /// <param name="adjacentNodes">Adjacent node list</param>
        public void AddNode(string id, string label, string[] adjacentNodes)
        {
            db.Connect(connectionParams);
            try
            {
                string ids = "";
                foreach (var nodeId in adjacentNodes)
                {
                    if (ids.Length > 0)
                        ids += ",";
                    ids += nodeId;
                }
                db.ExecScript(String.Format("CALL insert_node('{0}', '{1}', '{2}');", id, label, ids));
            }
            finally
            {
                db.Disconnect();
            }
        }

    }
}
