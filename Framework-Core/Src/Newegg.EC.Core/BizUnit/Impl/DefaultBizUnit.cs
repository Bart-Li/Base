using Newegg.EC.Core.Web.Context;

namespace Newegg.EC.Core.BizUnit.Impl
{
    [AutoSetupService(typeof(IBizUnit))]
    public class DefaultBizUnit : IBizUnit
    {
        /// <summary>
        /// Biz Unit default config file.
        /// </summary>
        private readonly IBizUnitConfigRepository _bizUnitConfig;

        /// <summary>
        /// Http context.
        /// </summary>
        private readonly IHttpContextRepository _httpContext;

        public DefaultBizUnit(IBizUnitConfigRepository bizUnitConfig, IHttpContextRepository httpContext)
        {
            this._bizUnitConfig = bizUnitConfig;
            this._httpContext = httpContext;
        }

        /// <summary>
        /// Gets biz unit name.
        /// </summary>
        public string Name
        {
            get
            {
                string result = string.Empty;

                if (!string.IsNullOrWhiteSpace(this.CountryCode))
                {
                    switch (this.CountryCode.Trim().ToUpper())
                    {
                        case "USB":
                            result = "B2B";
                            break;
                        default:
                            result = this.CountryCode.Trim().ToUpper();
                            break;
                    }
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
                var result = this.GetValueFromContext("CountryCode");
                if (string.IsNullOrWhiteSpace(result) && this._bizUnitConfig != null)
                {
                    result = this._bizUnitConfig.CountryCode;
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
                var result = this.GetValueFromContext("CompanyCode");
                if (string.IsNullOrWhiteSpace(result) && this._bizUnitConfig != null)
                {
                    return this._bizUnitConfig.CompanyCode;
                }

                return result.ToInt();
            }
        }

        /// <summary>
        /// Gets language code.
        /// </summary>
        public string LanguageCode
        {
            get
            {
                var result = this.GetValueFromContext("LanguageCode");
                if (string.IsNullOrWhiteSpace(result) && this._bizUnitConfig != null)
                {
                    result = this._bizUnitConfig.LanguageCode;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets country code.
        /// </summary>
        public string RegionCode
        {
            get
            {
                var result = this.GetValueFromContext("X-RegionCode");
                if (string.IsNullOrWhiteSpace(result) && this._bizUnitConfig != null)
                {
                    result = this._bizUnitConfig.RegionCode;
                }

                return result;
            }
        }

        /// <summary>
        /// Get string value by key.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <returns>Value string.</returns>
        private string GetValueFromContext(string key)
        {
            string result = string.Empty;
            if (this._httpContext != null)
            {
                return this._httpContext.QueryStringOrHeader(key);
            }

            return result;
        }
    }
}
