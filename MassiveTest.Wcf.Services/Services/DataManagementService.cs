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
        private IDataProvider provider;

        public DataManagementService(IDataProvider provider)
        {
            this.provider = provider;
        }

        public DataOperationResult AddNode(string id, string label, string[] adjacentIds)
        {
            provider.AddNode(id, label, adjacentIds);
            return DataOperationResult.Ok;
        }

        public DataOperationResult Clear()
        {
            provider.ClearNodes();
            return DataOperationResult.Ok;
        }

    }

}
