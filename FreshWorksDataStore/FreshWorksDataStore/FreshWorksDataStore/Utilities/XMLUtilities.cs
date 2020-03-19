using System;
using System.IO;
using System.Xml.Serialization;

namespace FreshWorksDataStore.Utilities
{
    public class XMLUtilities<T>
    {
        /// <summary>
        /// XML Serializer
        /// </summary>
        /// <param name="dataStore"></param>
        /// <param name="filePath"></param>
        public void Serialize(T dataStore, string filePath)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer serializer = Serializer.GetXMLSerializer(typeof(T));
            lock (serializer)
            {
                using (TextWriter writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, dataStore, ns);
                }
            }
        }

        /// <summary>
        /// Deserializer
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public T DeSerialize(string filePath)
        {
            object obj;
            XmlSerializer serializer = Serializer.GetXMLSerializer(typeof(T));
            lock (serializer)
            {
                using (TextReader reader = new StreamReader(filePath))
                {
                    if (reader.Peek() > 0)
                        obj = serializer.Deserialize(reader);
                    else
                        obj = Activator.CreateInstance(typeof(T));
                }
                return (T)obj;
            }
        }
    }

    /// <summary>
    /// Declare Serializer object thread safe
    /// </summary>
    public class Serializer
    {
        private static XmlSerializer xmlSerializer;
        private static readonly object obj = new object();

        private Serializer()
        {

        }

        public static XmlSerializer GetXMLSerializer(Type T)
        {
            lock (obj)
            {
                if (xmlSerializer == null)
                {
                    xmlSerializer = new XmlSerializer(T);
                }
                return xmlSerializer;
            }
        }
    }
}
