using System;
using System.Linq;
using Newegg.EC.Core.Configuration;
using Newegg.EC.Core.Host.Config;
using Newegg.EC.Core.Web.Context;

namespace Newegg.EC.Core.Host.Impl
{
    /// <summary>
    /// Server mapping respository.
    /// </summary>
    [AutoSetupService(typeof(IServerMappingRepository))]
    public class DefaultServerMappingRepository : IServerMappingRepository
    {
        /// <summary>
        /// Configuration manager.
        /// </summary>
        private readonly IConfigurationManager _configManager;

        /// <summary>
        /// Current enviroment.
        /// </summary>
        private readonly ICurrentEnvironment _currentEnviroment;

        /// <summary>
        /// The HTTP context.
        /// </summary>
        private readonly IHttpContextRepository _httpContext;

        /// <summary>
        /// Server mapping repository.
        /// </summary>
        /// <param name="configManager">Configuration manager.</param>
        /// <param name="currentEnviroment">Current enviroment.</param>
        /// <param name="httpContext">Http context.</param>
        public DefaultServerMappingRepository(IConfigurationManager configManager, ICurrentEnvironment currentEnviroment, IHttpContextRepository httpContext)
        {
            this._configManager = configManager;
            this._currentEnviroment = currentEnviroment;
            this._httpContext = httpContext;
        }

        /// <summary>
        /// Get current server name.
        /// </summary>
        public string CurrentServerName => this._currentEnviroment.MachineName;

        /// <summary>
        /// Get channel which current server belongs to.
        /// </summary>
        public string CurrentChannel => this._currentEnviroment.Channel;

        /// <summary>
        /// Get current server database.
        /// </summary>
        public string CurrentServerDatabase => this.TryGetServerInformation(out var currentServer) ? currentServer.QueryDB : string.Empty;

        /// <summary>
        /// Get current server history data base.
        /// </summary>
        public string CurrentServerHistoryDatabase => this.TryGetServerInformation(out var currentServer) ? currentServer.HisQueryDB : string.Empty;

        /// <summary>
        /// Try get server information.
        /// </summary>
        /// <param name="server">Server mapping unit.</param>
        /// <returns>Is find current server information.</returns>
        private bool TryGetServerInformation(out ServerMappingUnit server)
        {
            var serverConfig = this._configManager.GetConfigFromService<ServerMappingConfig>("ServerMapping");
            if (serverConfig == null || serverConfig.ServerList.IsNullOrEmpty())
            {
                server = null;
                return false;
            }

            var channelName = string.Empty;
            if (this._httpContext != null)
            {
                channelName = this._httpContext.QueryStringOrHeader("X-Channel");
            }

            if (string.IsNullOrWhiteSpace(channelName))
            {
                channelName = this._currentEnviroment.Channel;
            }

            server = serverConfig.ServerList.FirstOrDefault(s => s.Channel.Equals(channelName, StringComparison.OrdinalIgnoreCase));
            return server != null;
        }
    }
}
