using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MassiveTest.Interface;
using MassiveTest.Wcf.Services.Tools;
using System.Reflection;
using System.IO;

namespace MassiveTest.Wcf.Services.Settings
{
    public class DbConnectionParams : IDbConnectionParams
    {
        public string Host { get; set; }

        public string User { get; set; }

        public string Pass { get; set; }

        public string DbName { get; set; }

        public DbConnectionParams()
        {
            var cfg = DbConfig.GetInstance();
            Host = cfg.Host;
            User = cfg.User;
            Pass = cfg.Pass;
            DbName = cfg.DbName;
        }
    }

    [XmlRoot("params")]
    [Serializable]
    public class DbConfig
    {
        [XmlElement("host")]
        public string Host { get; set; }

        [XmlElement("user")]
        public string User { get; set; }

        [XmlElement("pass")]
        public string Pass { get; set; }

        [XmlElement("dbname")]
        public string DbName { get; set; }

        public DbConfig()
        {
            Host = "";
            User = "";
            Pass = "";
            DbName = "";
        }

        public static DbConfig GetInstance()
        {
            DbConfig instance = null;
            string fname = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "db.config");
            if (!File.Exists(fname))
            {
                instance = new DbConfig();
                XmlStorageProvider.SaveToFile<DbConfig>(fname, instance);
            }
            else
                instance = XmlStorageProvider.LoadFromFile<DbConfig>(fname);
            return instance;
        }
    }

}
