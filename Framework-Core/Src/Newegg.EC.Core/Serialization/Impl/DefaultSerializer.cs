using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Newegg.EC.Core.Serialization.Impl
{
    /// <summary>
    /// Default serializer.
    /// </summary>
    [AutoSetupService(typeof(ISerializer))]
    public class DefaultSerializer : ISerializer
    {
        /// <summary>
        /// Serializa object to json.
        /// </summary>
        /// <param name="value">object instance.</param>
        /// <returns>Json string.</returns>
        public string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// Deserialize json string to object.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="value">Json string.</param>
        /// <returns>Object instance.</returns>
        public T DeserializeObject<T>(string value)
        {
            if (typeof(T) == typeof(string))
            {
                return (T)((object)value);
            }

            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// Deserialize json string to object.
        /// </summary>
        /// <param name="value">Json string.</param>
        /// <returns>Object instance.</returns>
        public object DeserializeObject(string value)
        {
            return JsonConvert.DeserializeObject(value);
        }

        /// <summary>
        /// Serializa object to XML.
        /// </summary>
        /// <param name="value">object instance.</param>
        /// <returns>Json string.</returns>
        public string SerializeXml<T>(T value)
        {
            using (var memory = new MemoryStream())
            {
                using (TextReader reader = new StreamReader(memory))
                {
                    SerializeXmlStream(value, memory);
                    memory.Position = 0L;
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Serializa object to XML.
        /// </summary>
        /// <param name="value">Object instance.</param>
        /// <param name="stream">Object stream.</param>
        /// <returns>Xml string.</returns>
        public void SerializeXmlStream<T>(T value, Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, value);
        }

        /// <summary>
        /// Deserialize xml string to object.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="value">Xml string.</param>
        /// <returns>Object instance.</returns>
        public T DeserializeXml<T>(string value)
        {
            using (var memory = new MemoryStream())
            {
                using (TextWriter writer = new StreamWriter(memory))
                {
                    writer.Write(value);
                    writer.Flush();
                    return DeserializeXmlStream<T>(memory);
                }
            }
        }

        /// <summary>
        /// Deserialize data from XML stream.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="stream">Stream data.</param>
        /// <returns>Config type instance.</returns>
        public T DeserializeXmlStream<T>(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StreamReader(stream))
            {
                reader.ReadToEnd();
                stream.Position = 0;
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
