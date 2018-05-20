using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newegg.EC.Core.Host;
using Newegg.EC.Core.RestClient.Config;

namespace Newegg.EC.Core.RestClient.Impl
{
    public class ConfiguredRestfulRequest : DefaultRestfulRequest
    {
        /// <summary>
        /// Service host repository.
        /// </summary>
        private readonly IServiceHostRepository _serviceHostRepository;

        /// <summary>
        /// Restful config repository.
        /// </summary>
        private readonly IRestfulConfigRepository _restfulConfigRepository;

        /// <summary>
        /// Configured restful request repository.
        /// </summary>
        /// <param name="resourceKey">Resource key.</param>
        /// <param name="serviceHostRepository">Service host repository.</param>
        /// <param name="restfulConfigRepository">Restful config repository.</param>
        public ConfiguredRestfulRequest(string resourceKey, IServiceHostRepository serviceHostRepository, IRestfulConfigRepository restfulConfigRepository) 
            : base()
        {
            this._serviceHostRepository = serviceHostRepository;
            this._restfulConfigRepository = restfulConfigRepository;

            var resourceConfig = this._restfulConfigRepository.GetResource(resourceKey);
            if (resourceConfig == null)
            {
                throw new ArgumentException(string.Format(@"Not found rest resource key ""{0}"", please check the RestfulService.config.", resourceKey));
            }

            this.LoadResourceConfig(resourceConfig);
        }

        /// <summary>
        /// Load resource config.
        /// </summary>
        /// <param name="config"></param>
        protected void LoadResourceConfig(RestfulServiceResourceUnit config)
        {
            this.Method = config.Verb;
            this.Resource = config.Url;
            this.LoadDefaultConfig();
            this.LoadSettings(config);
            this.LoadResource(config);
        }

        #region Default Level

        /// <summary>
        /// Load default config.
        /// </summary>
        protected void LoadDefaultConfig()
        {
            this.LoadMaxResponseSize(this._restfulConfigRepository.DefaultMaxResponseSize);
            this.LoadTimeOut(this._restfulConfigRepository.DefaultTimeout);
            this.LoadRemoveDefaultParas(this._restfulConfigRepository.RemoveDefaultParameter);
        }

        #endregion

        #region Setting Level

        /// <summary>
        /// Load settings.
        /// </summary>
        /// <param name="config">Resource setting config.</param>
        protected void LoadSettings(RestfulServiceResourceUnit config)
        {
            if (string.IsNullOrEmpty(config.Setting))
            {
                return;
            }

            RestfulServiceSettingUnit setting = this._restfulConfigRepository.GetSetting(config.Setting);
            if (setting == null)
            {
                throw new ArgumentException(string.Format(@"Not found setting called ""{0}"", please check the RestfulService.config.", config.Setting));
            }

            this.LoadHost(setting.Host);
            this.LoadMaxResponseSize(setting.MaxResponseSize);
            this.LoadTimeOut(setting.TimeoutSpan);
            this.LoadRemoveDefaultParas(setting.RemoveDefaultParameter);
            this.LoadUrlParameters(setting.UrlParameters);
            this.LoadHeaders(setting.Headers);
        }

        #endregion

        #region Resource Level

        /// <summary>
        /// Load settings.
        /// </summary>
        /// <param name="config">Resource config.</param>
        protected void LoadResource(RestfulServiceResourceUnit config)
        {            
            this.LoadMaxResponseSize(config.MaxResponseSize);
            this.LoadTimeOut(config.TimeoutSpan);
            this.LoadRemoveDefaultParas(config.RemoveDefaultParameter);
            this.LoadUrlParameters(config.UrlParameters);
            this.LoadHeaders(config.Headers);
        }

        #endregion

        #region Load config value

        /// <summary>
        /// Load max response size.
        /// </summary>
        /// <param name="maxResponseSize">Max response size.</param>
        protected void LoadMaxResponseSize(long maxResponseSize)
        {
            if (maxResponseSize != default(long))
            {
                this.MaxResponseSize = maxResponseSize;
            }
        }

        /// <summary>
        /// Load time out.
        /// </summary>
        /// <param name="timeout">Time out.</param>
        protected void LoadTimeOut(TimeSpan timeout)
        {
            if (timeout != default(TimeSpan))
            {
                this.Timeout = timeout;
            }
        }

        /// <summary>
        /// Load host.
        /// </summary>
        /// <param name="hostName">Host name.</param>
        protected virtual void LoadHost(string hostName)
        {
            if (string.IsNullOrWhiteSpace(hostName))
            {
                return;
            }

            this.HostName = hostName;
            var serviceHost = this._serviceHostRepository.GetService(hostName);
            this.BaseUrl = serviceHost?.Address;
        }

        /// <summary>
        /// Load removeDefaultParas.
        /// </summary>
        /// <param name="removeDefaultParas">Remove Default Paras.</param>
        protected void LoadRemoveDefaultParas(bool removeDefaultParas)
        {
            if (removeDefaultParas && !this.RemoveDefaultParameter)
            {
                this.RemoveDefaultParameter = true;
            }
        }

        /// <summary>
        /// Load url parameters.
        /// </summary>
        /// <param name="urlParameters">Url parameters.</param>
        protected void LoadUrlParameters(IList<ParameterUnit> urlParameters)
        {
            if (!urlParameters.IsNullOrEmpty())
            {
                urlParameters.Where(item => !string.IsNullOrWhiteSpace(item.Name) && item.Value != null).ForEach(item =>
                {
                    this.AddUrlParameter(item.Name, item.Value);
                });
            }
        }

        /// <summary>
        /// Load headers.
        /// </summary>
        /// <param name="headers">Header values.</param>
        protected void LoadHeaders(IList<ParameterUnit> headers)
        {
            if (!headers.IsNullOrEmpty())
            {
                headers.Where(item => !string.IsNullOrWhiteSpace(item.Name) && item.Value != null).ForEach(item =>
                {
                    this.AddHeader(item.Name, item.Value);
                });
            }
        }

        #endregion
    }
}
