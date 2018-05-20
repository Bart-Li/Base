using System;
using System.Collections.Generic;

namespace Newegg.EC.Core.RestClient.Config
{
    /// <summary>
    /// Restful serivce config.
    /// </summary>
    public class RestfulServiceConfig
    {
        public string DefaultTimeout { get; set; }

        public TimeSpan DefaultTimeoutSpan
        {
            get {
                TimeSpan result;
                TimeSpan.TryParse(DefaultTimeout, out result);
                return result;
            }
        }

        public long DefaultMaxResponseSize { get; set; }

        public bool RemoveDefaultParameter { get; set; }

        public List<RestfulServiceResourceUnit> Resources { get; set; }

        public List<RestfulServiceSettingUnit> SettingGroups { get; set; }
    }

    public class RestfulServiceResourceUnit
    {
        public string Key { get; set; }

        public Method Verb { get; set; }

        public string Url { get; set; }

        public string Setting { get; set; }

        public string Timeout { get; set; }

        public TimeSpan TimeoutSpan
        {
            get
            {
                TimeSpan result;
                TimeSpan.TryParse(Timeout, out result);
                return result;
            }
        }

        public long MaxResponseSize { get; set; }

        public List<ParameterUnit> UrlParameters { get; set; }

        public List<ParameterUnit> Headers { get; set; }

        public bool RemoveDefaultParameter { get; set; }
    }

    public class RestfulServiceSettingUnit
    {
        public string Key { get; set; }

        public long MaxResponseSize { get; set; }

        public string Host { get; set; }

        public string Timeout { get; set; }

        public TimeSpan TimeoutSpan
        {
            get
            {
                TimeSpan result;
                TimeSpan.TryParse(Timeout, out result);
                return result;
            }
        }

        public List<ParameterUnit> UrlParameters { get; set; }

        public List<ParameterUnit> Headers { get; set; }

        public bool RemoveDefaultParameter { get; set; }
    }

    public class ParameterUnit
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
