using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassiveTest.Interface;

namespace MassiveTest.Wcf.Services.Settings
{
    public class DbConnectionParams : IDbConnectionParams
    {
        public string Host
        {
            get { return DbSettings.Default.DbHost; }
            set { }
        }

        public string User
        {
            get { return DbSettings.Default.DbUser; }
            set { }
        }

        public string Pass
        {
            get { return DbSettings.Default.DbPass; }
            set { }
        }

        public string DbName
        {
            get { return DbSettings.Default.DbName; }
            set { }
        }
    }
}
