using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Unity.Wcf;
using MassiveTest.Interface;
using MassiveTest.DataProvider;

namespace MassiveTest.Wcf.Services {
    public class WcfServiceFactory : UnityServiceHostFactory {

        protected override void ConfigureContainer(IUnityContainer container) {
            //container.LoadConfiguration();
            container.RegisterType<IGraph, MassiveTest.Graphs.Graph>();
            container.RegisterType<IDataProvider, DbDataProvider >();
            container.RegisterType<IDomainSpecificService, DomainSpecificService>();
            container.RegisterType<IDbConnectionParams, MassiveTest.Wcf.Services.Settings.DbConnectionParams>();
            container.RegisterType<IDbEngine, MassiveTest.DataAccess.MySql.MySqlEngine>();
            //UnityConfig.RegisterTypes(container);
        }
    }
}
