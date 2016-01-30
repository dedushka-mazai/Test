using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MassiveTest.DataLoader.Tools
{
    public class XmlStorageProvider
    {
        public static T LoadFromFile<T>(string fileName)
        {
            T res;
            XmlReader reader = null;
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                reader = new XmlTextReader(fileName);
                res = (T)xs.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return res;
        }
    }
}
