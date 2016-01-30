using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassiveTest.Interface;

namespace MassiveTest.Wcf.Services
{
    public class DataManagementService : IDataManagementService
    {
        public DataOperationResult AddNode(string id, string label, string[] adjacentIds)
        {
            return DataOperationResult.Ok;
        }

        public DataOperationResult Clear()
        {
            return DataOperationResult.Ok;
        }

    }

}
