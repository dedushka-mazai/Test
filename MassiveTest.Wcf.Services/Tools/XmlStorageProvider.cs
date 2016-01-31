using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MassiveTest.Wcf.Services.Tools
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
                var xs = new XmlSerializer(typeof(T));
                reader = new XmlTextReader(fileName);
                res = (T)xs.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return res;
        }

        /// <summary>
        /// Serializes and saves specified instance to the XML file
        /// </summary>
        /// <typeparam name="T">Instance type</typeparam>
        /// <param name="fileName">XML file to save instance to</param>
        /// <param name="instance">Instance of type T</param>
        public static void SaveToFile<T>(string fileName, T instance)
        {
            XmlWriter writer = null;
            try
            {
                var xs = new XmlSerializer(typeof(T));
                writer = XmlWriter.Create(fileName, new XmlWriterSettings() { Indent = true });
                xs.Serialize(writer, instance);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}
