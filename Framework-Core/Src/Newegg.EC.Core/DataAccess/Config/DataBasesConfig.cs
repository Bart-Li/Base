using System.Collections.Generic;
using System.Xml.Serialization;

namespace Newegg.EC.Core.DataAccess.Config
{
    [XmlRoot(ElementName = "DatabasesConfig")]
    public class DataBasesConfig
    {
        /// <summary>
        /// Gets or sets check context database.
        /// </summary>
        [XmlElement("checkContextDatabase")]
        public string CheckContextDatabase { get; set; }

        /// <summary>
        /// Gets or sets database groups.
        /// </summary>
        [XmlElement("dbGroup")]
        public List<DataBaseGroup> DatabaseGroups { get; set; }
    }
}
