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

        public void BuildGraph(IGraph graph)
        {
            graph.Clear();

            db.Connect(connectionParams);
            try
            {
                var reader = db.Execute("select n.id, n.label, an.adj_id from nodes n left outer join adjacent_nodes an on n.id = an.id order by 1");
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
    }
}
