using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Newegg.EC.Core.Serialization;

namespace Newegg.EC.Core.RestClient.Impl
{
    /// <summary>
    /// Default restful request.
    /// </summary>
    public class DefaultRestfulRequest : IRestfulRequest
    {
        private readonly ISerializer _serializer;
        private RequestBody _reuqestBody;
        private bool _needGenerateUrl;
        private string _internalUrl;
        private string _internalQueryString;

        /// <summary>
        /// Default restful request.
        /// </summary>
        public DefaultRestfulRequest()
        {
            this.Method = Method.GET;
            this.RequestFormat = DataFormat.Json;
            this.Parameters = new List<Parameter>();
            this._serializer = ECLibraryContainer.Get<ISerializer>();
            this._needGenerateUrl = true;            
        }

        /// <summary>
        /// Determines what HTTP method to use for this request. Supported methods: GET, POST, PUT, DELETE, HEAD, OPTIONS.
        /// Default is GET
        /// </summary>
        public Method Method { get; set; }

        /// <summary>
        /// The Resource URL to make the request against.
        /// </summary>
        public string Resource { get; set; }

        /// <summary>
        /// Gets or sets base url.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets max response size.
        /// </summary>
        public long MaxResponseSize { get; set; }

        /// <summary>
        /// Timeout to be used for the request.
        /// </summary>
        public TimeSpan Timeout { get; set; } = new TimeSpan(0, 0, 60);

        /// <summary>
        /// Gets or sets web proxy.
        /// </summary>
        public RestfulWebProxy WebProxy { get; set; }

        /// <summary>
        /// A value indicating whether remove default parameters.
        /// </summary>
        public bool RemoveDefaultParameter { get; set; }

        /// <summary>
        /// Serializer to use when writing JSON request bodies. Used if RequestFormat is JSON.
        /// By default JsonSerializer is used.
        /// </summary>
        public DataFormat RequestFormat { get; set; }

        /// <summary>
        /// Gets or sets host name from service host.config.
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// Gets complete url.
        /// </summary>
        public string Url
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.BaseUrl))
                {
                    throw new OperationCanceledException("Please set a valid base url.");
                }

                this.BuilderUrl();
                return this._internalUrl;
            }
        }

        /// <summary>
        /// Gets final resource query string.
        /// </summary>
        public string QueryString
        {
            get
            {
                this.BuilderUrl();
                return this._internalQueryString;
            }
        }

        /// <summary>
        /// Gets parameter collection.
        /// </summary>
        public IList<Parameter> Parameters { get; }

        /// <summary>
        /// Gets url parameter collection.
        /// </summary>
        public IList<KeyValuePair<string, string>> UrlParameters => this.FilterParameter(ParameterType.QueryString);

        /// <summary>
        /// Gets head collection.
        /// </summary>
        public IList<KeyValuePair<string, string>> Headers => this.FilterParameter(ParameterType.HttpHeader);

        /// <summary>
        /// Gets cookie collection.
        /// </summary>
        public IList<KeyValuePair<string, string>> Cookies => this.FilterParameter(ParameterType.Cookie);

        /// <summary>
        /// Gets request body object.
        /// </summary>
        public RequestBody RequestBody => this._reuqestBody;

        /// <summary>
        /// Serializes obj to data format specified by RequestFormat and adds it to the request body.
        /// The default format is JSON. Change RequestFormat if you wish to use a different serialization format.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="requestFormat">The request data format.</param>
        /// <returns>This request.</returns>
        public IRestfulRequest AddBody(object obj, DataFormat requestFormat = DataFormat.Json)
        {
            this.RequestFormat = requestFormat;
            var requestBody = new RequestBody { Body = obj };
            switch (requestFormat)
            {
                case DataFormat.Json:
                    requestBody.ContentType = ContentType.Json;
                    requestBody.Serialized = this._serializer.SerializeObject(obj);
                    break;
                case DataFormat.Xml:
                    requestBody.ContentType = ContentType.Xml;
                    requestBody.Serialized = this._serializer.SerializeXml(obj);
                    break;
                default:
                    break;
            }

            this._reuqestBody = requestBody;
            return this;
        }

        /// <summary>
        /// Adds or update a parameter to the request. There are four types of parameters:
        /// - Post: Either a encoded form value based on method
        /// - HttpHeader: Adds the name/value pair to the HTTP request's Headers collection
        /// - QueryString: Adds the name/value pair to the HTTP query string collection
        /// - Cookie: Adds the name/value pair to the HTTP request's Cookies collection
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <param name="value">Parameter value.</param>
        /// <param name="type">Parameter type.</param>
        public IRestfulRequest AddParameter(string name, string value, ParameterType type = ParameterType.Post)
        {
            name = name.Trim();
            value = string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();

            var parameter = Parameters.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && p.Type == type);

            if (parameter != null)
            {
                parameter.Value = value;
                parameter.Type = type;
            }
            else
            {
                Parameters.Add(new Parameter
                {
                    Name = name,
                    Value = value,
                    Type = type
                });
            }

            this._needGenerateUrl = true;
            return this;
        }

        /// <summary>
        /// Add or update url parameter.
        /// Shortcut to AddParameter(name, value, QueryString) overload.
        /// </summary>
        /// <param name="name">Url parameter name.</param>
        /// <param name="value">Url parameter value.</param>
        public IRestfulRequest AddUrlParameter(string name, string value)
        {
            this._needGenerateUrl = true;
            return AddParameter(name, value, ParameterType.QueryString);
        }

        /// <summary>
        /// Add or update header.
        /// Shortcut to AddParameter(name, value, HttpHeader) overload.
        /// </summary>
        /// <param name="name">Header name.</param>
        /// <param name="value">Header value.</param>
        public IRestfulRequest AddHeader(string name, string value)
        {
            return AddParameter(name, value, ParameterType.HttpHeader);
        }

        /// <summary>
        /// Add or update cookie.
        /// Shortcut to AddParameter(name, value, Cookie) overload.
        /// </summary>
        /// <param name="name">Cookie name.</param>
        /// <param name="value">Cookie value.</param>
        public IRestfulRequest AddCookie(string name, string value)
        {
            return AddParameter(name, value, ParameterType.Cookie);
        }

        /// <summary>
        /// Filter parameter.
        /// </summary>
        /// <param name="type">Parameter type.</param>
        /// <returns>Parameters.</returns>
        private IList<KeyValuePair<string, string>> FilterParameter(ParameterType type)
        {
            return Parameters?.Where(p => p.Type == type)
                .Select(p => new KeyValuePair<string, string>(p.Name, p.Value.ToString())).ToList();
        }

        /// <summary>
        /// Builder service url.
        /// </summary>
        private void BuilderUrl()
        {
            if (!this._needGenerateUrl)
            {
                return;
            }

            var host = this.BaseUrl.TrimEnd('/');

            if (!string.IsNullOrWhiteSpace(this.BaseUrl) && this.BaseUrl.IndexOf('?') > 0)
            {
                host = this.BaseUrl.Substring(0, this.BaseUrl.IndexOf('?'));
                var queryString = this.BaseUrl.Substring(this.BaseUrl.IndexOf('?') + 1);

                if (!Regex.IsMatch(queryString, @".+=.+(&.+=.+)*&?"))
                {
                    throw new FormatException(string.Format(@"Query string ""{0}"" is an invalid query string.", queryString));
                }

                queryString.Split('&').Where(item => !string.IsNullOrWhiteSpace(item))
                    .ForEach(item =>
                    {
                        var queryStringItem = item.Split('=');
                        this.AddUrlParameter(queryStringItem[0], queryStringItem[1]);
                    });
            }

            var url = host + "/" + (string.IsNullOrWhiteSpace(this.Resource) ? string.Empty : this.Resource.Trim('/'));
            var urlSgements = new List<string>();
            this.UrlParameters.ForEach(p =>
            {
                var segment = "{" + p.Key + "}";
                if (url.IndexOf(segment, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    url = url.Replace(segment, HttpUtility.UrlEncode(p.Value));
                    urlSgements.Add(p.Key);
                }
            });

            this._internalQueryString = BuilderQueryString(urlSgements);
            this._internalUrl = url.TrimEnd('/').TrimEnd('?') + "?" + this._internalQueryString;
            this._needGenerateUrl = false;
        }

        /// <summary>
        /// Builder query string.
        /// </summary>
        /// <param name="urlSgements">Url sgements.</param>
        /// <returns>Query string.</returns>
        private string BuilderQueryString(List<string> urlSgements)
        {            
            var sb = new StringBuilder();
            this.UrlParameters.Where(p => !urlSgements.Contains(p.Key)).ForEach(p =>
                {
                    if (sb.Length == 0)
                    {
                        sb.AppendFormat("{0}={1}", p.Key, HttpUtility.UrlEncode(p.Value));

                    }
                    else
                    {
                        sb.AppendFormat("&{0}={1}", p.Key, HttpUtility.UrlEncode(p.Value));
                    }
                });

            return sb.ToString();
        }
    }
}
