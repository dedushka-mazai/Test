using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassiveTest.Interface;

namespace MassiveTest.DataProvider
{
    /// <summary>
    /// Provides access to manually created data used for tests
    /// </summary>
    public class TestDataProvider
    {
        public void BuildGraph(IGraph g)
        {
            g.Clear();

            g.AddNode("1", "Apple", new string[3] { "3", "2", "11" });
            g.AddNode("2", "Intel", new string[5] { "1", "10", "5", "9", "7" });
            g.AddNode("3", "Google", new string[1] { "5" });
            g.AddNode("4", "IBM", null);
            g.AddNode("5", "PayPal", new string[4] { "2", "8", "5", "7" });
            g.AddNode("6", "eBay", new string[2] { "10", "7" });
            g.AddNode("7", "Microsoft", new string[1] { "6" });
            g.AddNode("8", "Yahoo", new string[1] { "9" });
            g.AddNode("9", "Amazon", new string[1] { "10" });
            g.AddNode("10", "Facebook", new string[1] { "9" });
        }

        void ClearNodes() { }
        void AddNode(string id, string label, string[] adjacentNodes) { }

    }
}
