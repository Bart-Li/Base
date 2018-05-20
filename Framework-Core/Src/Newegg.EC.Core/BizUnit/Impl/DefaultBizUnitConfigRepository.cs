using Newegg.EC.Core.Configuration;

namespace Newegg.EC.Core.BizUnit.Impl
{
    /// <summary>
    /// Default biz unit config respository.
    /// </summary>
    [AutoSetupService(typeof(IBizUnitConfigRepository))]
    public class DefaultBizUnitConfigRepository : IBizUnitConfigRepository
    {
        /// <summary>
        /// Configuration manager.
        /// </summary>
        private readonly IConfigurationManager _configManager;

        public DefaultBizUnitConfigRepository(IConfigurationManager configManager)
        {
            this._configManager = configManager;
        }

        /// <summary>
        /// Gets name.
        /// </summary>
        public string Name
        {
            get
            {
                string result = string.Empty;
                BizUnitConfig config = this.GetBizConfig();
                if (config != null)
                {
                    result = config.Name;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets country code.
        /// </summary>
        public string CountryCode
        {
            get
            {
                string result = string.Empty;
                BizUnitConfig config = this.GetBizConfig();
                if (config != null)
                {
                    result = config.CountryCode;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets company code.
        /// </summary>
        public int CompanyCode
        {
            get
            {
                int result = 0;
                BizUnitConfig config = this.GetBizConfig();
                if (config != null)
                {
                    result = config.CompanyCode;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets language code.
        /// </summary>
        public string LanguageCode
        {
            get
            {
                string result = string.Empty;
                BizUnitConfig config = this.GetBizConfig();
                if (config != null)
                {
                    result = config.LanguageCode;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets region code.
        /// </summary>
        public string RegionCode
        {
            get
            {
                string result = string.Empty;
                BizUnitConfig config = this.GetBizConfig();
                if (config != null)
                {
                    result = config.RegionCode;
                }

                if (string.IsNullOrWhiteSpace(result))
                {
                    result = config.CountryCode;
                }

                return result;
            }
        }

        /// <summary>
        /// Biz config.
        /// </summary>
        /// <returns></returns>
        private BizUnitConfig GetBizConfig()
        {
            return this._configManager.GetConfigFromService<BizUnitConfig>("BizUnit");
        }
    }
}
