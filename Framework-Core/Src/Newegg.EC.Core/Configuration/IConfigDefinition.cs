namespace Newegg.EC.Core.Configuration
{
    public interface IConfigDefinition
    {
        string ConfigKey { get; set; }

        string ConfigName { get; set; }

        string SystemName { get; set; }

        bool IsFromService { get; set; }
    }
}
