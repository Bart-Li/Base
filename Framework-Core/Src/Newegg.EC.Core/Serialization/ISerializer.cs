using System.IO;

namespace Newegg.EC.Core.Serialization
{
    public interface ISerializer
    {
        #region Json

        /// <summary>
        /// Serializa object to JSON.
        /// </summary>
        /// <param name="value">object instance.</param>
        /// <returns>Json string.</returns>
        string SerializeObject(object value);

        /// <summary>
        /// Deserialize JSON string to object.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="value">Json string.</param>
        /// <returns>Object instance.</returns>
        T DeserializeObject<T>(string value);

        /// <summary>
        /// Deserialize JSON string to object.
        /// </summary>
        /// <param name="value">Json string.</param>
        /// <returns>Object instance.</returns>
        object DeserializeObject(string value);

        #endregion

        #region Xml

        /// <summary>
        /// Serializa object to XML.
        /// </summary>
        /// <param name="value">object instance.</param>
        /// <returns>Json string.</returns>
        string SerializeXml<T>(T value);

        /// <summary>
        /// Serializa object to XML.
        /// </summary>
        /// <param name="value">Object instance.</param>
        /// <param name="stream">Object stream.</param>
        /// <returns>Xml string.</returns>
        void SerializeXmlStream<T>(T value, Stream stream);

        /// <summary>
        /// Deserialize xml string to object.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="value">Xml string.</param>
        /// <returns>Object instance.</returns>
        T DeserializeXml<T>(string value);

        /// <summary>
        /// Deserialize data from XML stream.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="stream">Stream data.</param>
        /// <returns>Config type instance.</returns>
        T DeserializeXmlStream<T>(Stream stream);

        #endregion
    }
}
