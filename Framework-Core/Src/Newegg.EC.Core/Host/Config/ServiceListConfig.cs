using System.Collections.Generic;

namespace Newegg.EC.Core.Host.Config
{
    public class ServiceListConfig
    {
        public List<ServiceUnit> Services { get; set; }
    }

    public class ServiceUnit
    {
        public string Name { get; set; }

        public List<ServiceHostUnit> Host { get; set; }
    }

    public class ServiceHostUnit
    {
        public string Address { get; set; }

        public string Channel { get; set; }
    }
}
