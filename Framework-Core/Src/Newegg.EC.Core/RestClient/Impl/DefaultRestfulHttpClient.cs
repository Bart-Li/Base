using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newegg.EC.Core.Serialization;

namespace Newegg.EC.Core.RestClient.Impl
{
    /// <summary>
    /// Http client restful client.
    /// </summary>
    [AutoSetupService(typeof(IRestfulHttpClient))]
    public class DefaultRestfulHttpClient : IRestfulHttpClient
    {
        private static HttpClient _httpClient;
        private readonly ISerializer _serializer;

        public DefaultRestfulHttpClient(ISerializer serializer)
        {
            this._serializer = serializer;
        }

        #region Send request sync

        /// <summary>
        /// Send request sync.
        /// </summary>
        /// <param name="request">Restful request.</param>
        /// <returns>Restful response.</returns>
        public IRestfulResponse Send(IRestfulRequest request)
        {
            return this.SendAsync(request).Result;
        }

        /// <summary>
        /// Send request sync.
        /// </summary>
        /// <typeparam name="TResponse">Type of response body.</typeparam>
        /// <param name="request">Restful request.</param>
        /// <returns>Restful response.</returns>
        public IRestfulResponse<TResponse> Send<TResponse>(IRestfulRequest request)
        {
            return this.SendAsync<TResponse>(request).Result;
        }

        #endregion

        #region Send request async

        /// <summary>
        /// Send request async.
        /// </summary>
        /// <param name="request">Restful request.</param>
        /// <returns>Restful response.</returns>
        public Task<IRestfulResponse> SendAsync(IRestfulRequest request)
        {
            switch (request.Method)
            {
                case Method.POST:
                case Method.PUT:
                case Method.PATCH:
                    return Execute(request, CreateHttpRequestMessageAsPost);
                default:
                    return Execute(request, CreateHttpRequestMessageAsGet);
            }
        }

        /// <summary>
        /// Send request async.
        /// </summary>
        /// <typeparam name="TResponse">Type of response body.</typeparam>
        /// <param name="request">Restful request.</param>
        /// <returns>Restful response.</returns>
        public Task<IRestfulResponse<TResponse>> SendAsync<TResponse>(IRestfulRequest request)
        {
            return DeserializeResponse<TResponse>(SendAsync(request));
        }

        #endregion

        private async Task<IRestfulResponse> Execute(IRestfulRequest restfulRequest, Func<IRestfulRequest, HttpRequestMessage> getHttpRequestMessage)
        {
            IRestfulResponse restfulResponse = new DefaultRestfulResponse { Request = restfulRequest };
            try
            {
                HttpClient httpClient = this.CreateHttpClient(restfulRequest);
                HttpRequestMessage httpRequestMessage = getHttpRequestMessage(restfulRequest);
                var cancellTokenSource = new CancellationTokenSource(restfulRequest.Timeout);
                HttpResponseMessage httpResponseMessage =
                    await httpClient.SendAsync(httpRequestMessage, cancellTokenSource.Token);
                httpResponseMessage.EnsureSuccessStatusCode();
                restfulResponse.IsSuccessful = httpResponseMessage.IsSuccessStatusCode;
                restfulResponse.StatusCode = httpResponseMessage.StatusCode;
                httpResponseMessage.Headers.ForEach(h => restfulResponse.Headers.Add(new KeyValuePair<string, string>(h.Key, h.Value.ToString())));
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    HttpContent content = httpResponseMessage.Content;
                    restfulResponse.RawBytes = await content.ReadAsByteArrayAsync();
                    restfulResponse.Content = await content.ReadAsStringAsync();
                }

                return restfulResponse;
            }
            catch (WebException webException)
            {
                restfulResponse.ErrorMessage = webException.Message;
                restfulResponse.ErrorException = webException;
                restfulResponse.StatusDescription = webException.Status.ToString();
                return restfulResponse;
            }
            catch (Exception e)
            {
                restfulResponse.ErrorMessage = e.Message;
                restfulResponse.ErrorException = e;
                restfulResponse.StatusDescription = $"Send request to {restfulRequest.Method}: {restfulRequest.Url} failed";
                return restfulResponse;
            }
        }

        private async Task<IRestfulResponse<TResponse>> DeserializeResponse<TResponse>(Task<IRestfulResponse> asyncResponse)
        {
            IRestfulResponse response = await asyncResponse;
            IRestfulResponse<TResponse> restfulResponse = new DefaultRestfulResponse<TResponse>(response);
            try
            {
                if (restfulResponse.IsSuccessful && !string.IsNullOrWhiteSpace(restfulResponse.Content))
                {
                    restfulResponse.ResponseBody = this._serializer.DeserializeObject<TResponse>(response.Content);
                }

                return restfulResponse;
            }
            catch (Exception e)
            {
                restfulResponse.ErrorMessage = e.Message;
                restfulResponse.ErrorException = e;
                restfulResponse.StatusDescription = "Deserialize response failed";
                return restfulResponse;
            }
        }

        private HttpRequestMessage CreateHttpRequestMessageAsGet(IRestfulRequest restfulRequest)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod(restfulRequest.Method.ToString()), restfulRequest.Url);
            this.ConfigureHttpHeader(restfulRequest, requestMessage);
            return requestMessage;
        }

        private HttpRequestMessage CreateHttpRequestMessageAsPost(IRestfulRequest restfulRequest)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod(restfulRequest.Method.ToString()), restfulRequest.Url);
            this.ConfigureHttpHeader(restfulRequest, requestMessage);
            if (restfulRequest.RequestBody != null)
            {
                requestMessage.Content = new StringContent(restfulRequest.RequestBody.Serialized, Encoding.UTF8, restfulRequest.RequestBody.ContentType);
            }
            else if (restfulRequest.Parameters.Any(p => p.Type == ParameterType.Post))
            {
                var content = string.Join("&", restfulRequest.Parameters.Where(p => p.Type == ParameterType.Post).Select(p => p.ToString()));
                requestMessage.Content = new StringContent(content, Encoding.UTF8, ContentType.Form);
            }

            return requestMessage;
        }

        /// <summary>
        /// Configure http header.
        /// </summary>
        /// <param name="restfulRequest">Restful request.</param>
        /// <param name="httpRequestMessage">Http request message.</param>
        private void ConfigureHttpHeader(IRestfulRequest restfulRequest, HttpRequestMessage httpRequestMessage)
        {
            httpRequestMessage.Headers.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
            restfulRequest.Headers.ForEach(h =>
            {
                httpRequestMessage.Headers.TryAddWithoutValidation(h.Key, h.Value);
            });

            if (!restfulRequest.Cookies.IsNullOrEmpty())
            {
                httpRequestMessage.Headers.TryAddWithoutValidation("cookie", string.Join(";",
                    restfulRequest.Cookies.Select(cookie => string.Concat(cookie.Key, "=", cookie.Value))));
            }

            if (restfulRequest.RequestBody != null)
            {
                httpRequestMessage.Headers.TryAddWithoutValidation("accept", restfulRequest.RequestBody.ContentType);
            }
        }

        /// <summary>
        /// Create http client.
        /// </summary>
        /// <returns>Http client.</returns>
        private HttpClient CreateHttpClient(IRestfulRequest restfulRequest)
        {
            if (_httpClient?.DefaultRequestHeaders != null && !_httpClient.DefaultRequestHeaders.ConnectionClose.HasValue)
            {
                return _httpClient;
            }

            var webProxy = new WebProxy { UseDefaultCredentials = true };
            if (restfulRequest.WebProxy != null && !string.IsNullOrEmpty(restfulRequest.WebProxy.Address))
            {
                webProxy = new WebProxy(restfulRequest.WebProxy.Address, true)
                {
                    BypassList = new string[] { restfulRequest.WebProxy.BypassList }
                };
            }

            var messageHandler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip,
                UseProxy = true,
                Proxy = webProxy
            };

            _httpClient = new HttpClient(messageHandler);
            _httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");

            if (restfulRequest.MaxResponseSize != default(long))
            {
                _httpClient.MaxResponseContentBufferSize = restfulRequest.MaxResponseSize;
            }

            return _httpClient;
        }
    }
}
