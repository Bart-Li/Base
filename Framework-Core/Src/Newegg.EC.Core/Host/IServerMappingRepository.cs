namespace Newegg.EC.Core.Host
{
    /// <summary>
    /// Server mapping repository.
    /// </summary>
    public interface IServerMappingRepository
    {
        /// <summary>
        /// Get current server name.
        /// </summary>
        string CurrentServerName { get; }

        /// <summary>
        /// Get channel which current server belongs to.
        /// </summary>
        string CurrentChannel { get; }

        /// <summary>
        /// Get current server database.
        /// </summary>
        string CurrentServerDatabase { get; }

        /// <summary>
        /// Get current server history data base.
        /// </summary>
        string CurrentServerHistoryDatabase { get; }
    }
}
