using System;
using System.Collections.Generic;
using System.Net;

namespace Newegg.EC.Core.RestClient.Impl
{
    public class DefaultRestfulResponse : IRestfulResponse
    {
        public DefaultRestfulResponse()
        {
            this.Headers = new List<KeyValuePair<string, string>>();
        }

        /// <summary>
        /// The RestRequest that was made to get this RestResponse
        /// </summary>
        /// <remarks>
        /// Mainly for debugging if ResponseStatus is not OK
        /// </remarks> 
        public IRestfulRequest Request { get; set; }
        /// <summary>
        /// String representation of response content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets response body bytes.
        /// </summary>
        public byte[] RawBytes { get; set; }

        /// <summary>
        /// Gets head collection.
        /// </summary>
        public IList<KeyValuePair<string, string>> Headers { get; set; }

        /// <summary>
        /// HTTP response status code
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Whether or not the response status code indicates success
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Description of HTTP status returned.
        /// </summary>
        public string StatusDescription { get; set; }

        /// <summary>
        /// Transport or other non-HTTP error generated while attempting request
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Exceptions thrown during the request, if any.  
        /// </summary>
        /// <remarks>Will contain only network transport or framework exceptions thrown during the request.
        /// HTTP protocol errors are handled by RestSharp and will not appear here.</remarks>
        public Exception ErrorException { get; set; }
    }

    public class DefaultRestfulResponse<T> : DefaultRestfulResponse, IRestfulResponse<T>
    {
        public DefaultRestfulResponse()
        {
        }

        public DefaultRestfulResponse(IRestfulResponse response)
        {
            this.Request = response.Request;
            this.Content = response.Content;
            this.RawBytes = response.RawBytes;
            this.Headers = response.Headers;
            this.StatusCode = response.StatusCode;
            this.IsSuccessful = response.IsSuccessful;
            this.ErrorMessage = response.ErrorMessage;
            this.ErrorException = response.ErrorException;
            this.StatusDescription = response.StatusDescription;
        }

        public T ResponseBody { get; set; }
    }
}
