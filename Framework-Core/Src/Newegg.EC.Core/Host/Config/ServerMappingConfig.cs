using System.Collections.Generic;

namespace Newegg.EC.Core.Host.Config
{
    public class ServerMappingConfig
    {
        public List<ServerMappingUnit> ServerList { get; set; }
    }

    public class ServerMappingUnit
    {
        public string Channel { get; set; }

        public string QueryDB { get; set; }

        public string HisQueryDB { get; set; }
    }
}
