using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Newegg.EC.Core.DataAccess.Config
{
    /// <summary>
    /// Gets or sets data commands config.
    /// </summary>
    [XmlRoot(ElementName = "DataCommandsConfig")]
    public class DataCommandsConfig
    {
        /// <summary>
        /// Gets or sets data command list.
        /// </summary>
        [XmlElement("dataCommand")]
        public List<DataCommandUnit> DataCommandCollection { get; set; }
    }
}
