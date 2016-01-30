using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MassiveTest.DataLoader.Tools
{
    /// <summary>
    /// Deserializes instance of the specified class from the XML file
    /// </summary>
    public class XmlStorageProvider
    {
        /// <summary>
        /// Loads and deserializes instance of the specified class from the XML file
        /// </summary>
        /// <typeparam name="T">Instance type</typeparam>
        /// <param name="fileName">XML file, where instance will be loaded from</param>
        /// <returns>Deserialized instance of type T</returns>
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
