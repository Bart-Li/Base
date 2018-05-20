namespace Newegg.EC.Core.Configuration
{
    /// <summary>
    /// Configuration accessor.
    /// </summary>
    public interface IConfigAccessor
    {
        TConfigType GetConfigValue<TConfigType>(IConfigDefinition configDefinition, NodeDataType nodeDataType = NodeDataType.Json) where TConfigType : class, new();

        TConfigType GetConfigSection<TConfigType>(string sectionName) where TConfigType : class, new();
    }
}
