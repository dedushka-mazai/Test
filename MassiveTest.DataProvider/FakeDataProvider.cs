using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassiveTest.Interface;

namespace MassiveTest.DataProvider
{
    /// <summary>
    /// Simple stub provider
    /// </summary>
    public class FakeDataProvider : IDataProvider
    {
        public void BuildGraph(IGraph graph)
        {
            // do nothing
        }
    }
}
