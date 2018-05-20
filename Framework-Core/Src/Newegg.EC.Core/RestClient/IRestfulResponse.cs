using System;
using System.Collections.Generic;
using System.Net;

namespace Newegg.EC.Core.RestClient
{
    /// <summary>
    /// Container for data sent back from API.
    /// </summary>
    public interface IRestfulResponse
    {
        /// <summary>
        /// The RestRequest that was made to get this RestResponse
        /// </summary>
        /// <remarks>
        /// Mainly for debugging if ResponseStatus is not OK
        /// </remarks> 
        IRestfulRequest Request { get; set; }

        /// <summary>
        /// String representation of response content
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// Gets response body bytes.
        /// </summary>
        byte[] RawBytes { get; set; }

        /// <summary>
        /// Gets head collection.
        /// </summary>
        IList<KeyValuePair<string, string>> Headers { get; set; }

        /// <summary>
        /// HTTP response status code
        /// </summary>
        HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Whether or not the response status code indicates success
        /// </summary>
        bool IsSuccessful { get; set; }

        /// <summary>
        /// Description of HTTP status returned.
        /// </summary>
        string StatusDescription { get; set; }

        /// <summary>
        /// Transport or other non-HTTP error generated while attempting request
        /// </summary>
        string ErrorMessage { get; set; }

        /// <summary>
        /// Exceptions thrown during the request, if any.  
        /// </summary>
        /// <remarks>Will contain only network transport or framework exceptions thrown during the request.
        /// HTTP protocol errors are handled by RestSharp and will not appear here.</remarks>
        Exception ErrorException { get; set; }
    }

    /// <summary>
    /// Container for data sent back from API including deserialized data.
    /// </summary>
    /// <typeparam name="T">Type of data to deserialize to</typeparam>
    public interface IRestfulResponse<T> : IRestfulResponse
    {
        T ResponseBody { get; set; }
    }
}
