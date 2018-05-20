namespace Newegg.EC.Core.Configuration.Impl
{
    /// <summary>
    /// Config definition.
    /// </summary>
    public class ConfigDefinition : IConfigDefinition
    {
        public string ConfigKey { get; set; }

        public string ConfigName { get; set; }

        public string SystemName { get; set; }

        public bool IsFromService { get; set; }
    }
}
