using System;
using System.Collections.Generic;
using System.Text;

namespace Newegg.EC.Core.DataAccess.Config
{
    public class DataAccessConfig
    {
        public string SystemName { get; set; }

        public string DatabaseConfig { get; set; }

        public List<string> DataCommandConfig { get; set; }
    }
}
