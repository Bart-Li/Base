using System;
using System.Collections.Generic;
using Newegg.EC.Core.RestClient.Impl;

namespace Newegg.EC.Core.RestClient
{
    public interface IRestfulRequest
    {
        /// <summary>
        /// Determines what HTTP method to use for this request. Supported methods: GET, POST, PUT, DELETE, HEAD, OPTIONS.
        /// Default is GET
        /// </summary>
        Method Method { get; set; }

        /// <summary>
        /// The Resource URL to make the request against.
        /// </summary>
        string Resource { get; set; }

        /// <summary>
        /// Gets or sets base url.
        /// </summary>
        string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets max response size.
        /// </summary>
        long MaxResponseSize { get; set; }

        /// <summary>
        /// Timeout to be used for the request.
        /// </summary>
        TimeSpan Timeout { get; set; }

        /// <summary>
        /// Gets or sets web proxy.
        /// </summary>
        RestfulWebProxy WebProxy { get; set; }

        /// <summary>
        /// A value indicating whether remove default parameters.
        /// </summary>
        bool RemoveDefaultParameter { get; set; }

        /// <summary>
        /// Serializer to use when writing JSON request bodies. Used if RequestFormat is JSON.
        /// By default JsonSerializer is used.
        /// </summary>
        DataFormat RequestFormat { get; set; }

        /// <summary>
        /// Gets or sets main host name from service host.config.
        /// </summary>
        string HostName { get; set; }

        /// <summary>
        /// Gets final resource query string.
        /// </summary>
        string QueryString { get; }

        /// <summary>
        /// Gets complete url.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// Gets parameter collection.
        /// </summary>
        IList<Parameter> Parameters { get; }

        /// <summary>
        /// Gets url parameter collection.
        /// </summary>
        IList<KeyValuePair<string, string>> UrlParameters { get; }

        /// <summary>
        /// Gets head collection.
        /// </summary>
        IList<KeyValuePair<string, string>> Headers { get; }

        /// <summary>
        /// Gets cookie collection.
        /// </summary>
        IList<KeyValuePair<string, string>> Cookies { get; }

        /// <summary>
        /// Gets request body object.
        /// </summary>
        RequestBody RequestBody { get; }

        /// <summary>
        /// Serializes obj to data format specified by RequestFormat and adds it to the request body.
        /// The default format is JSON. Change RequestFormat if you wish to use a different serialization format.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="requestFormat">The request data format.</param>
        /// <returns>This request.</returns>
        IRestfulRequest AddBody(object obj, DataFormat requestFormat = DataFormat.Json);

        /// <summary>
        /// Adds or update a parameter to the request. There are four types of parameters:
        /// <para>Post: Adds the name/value pair to the HTTP form encoded value.</para>
        /// <para>HttpHeader: Adds the name/value pair to the HTTP request's Headers collection.</para>
        /// <para>QueryString: Adds the name/value pair to the HTTP query string collection.</para>
        /// <para>Cookie: Adds the name/value pair to the HTTP request's Cookies collection.</para>
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <param name="value">Parameter value.</param>
        /// <param name="type">Parameter type.</param>
        IRestfulRequest AddParameter(string name, string value, ParameterType type = ParameterType.Post);

        /// <summary>
        /// Add or update url parameter.
        /// Shortcut to AddParameter(name, value, QueryString) overload.
        /// </summary>
        /// <param name="name">Url parameter name.</param>
        /// <param name="value">Url parameter value.</param>
        IRestfulRequest AddUrlParameter(string name, string value);

        /// <summary>
        /// Add or update header.
        /// Shortcut to AddParameter(name, value, HttpHeader) overload.
        /// </summary>
        /// <param name="name">Header name.</param>
        /// <param name="value">Header value.</param>
        IRestfulRequest AddHeader(string name, string value);

        /// <summary>
        /// Add or update cookie.
        /// Shortcut to AddParameter(name, value, Cookie) overload.
        /// </summary>
        /// <param name="name">Cookie name.</param>
        /// <param name="value">Cookie value.</param>
        IRestfulRequest AddCookie(string name, string value);
    }
}
