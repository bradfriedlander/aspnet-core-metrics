using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace demoWebApp.HttpHelpers
{
    public class HttpRequestBuilder
    {
        public HttpRequestBuilder()
        {
        }

        private string acceptHeader = "application/json";
        private bool allowAutoRedirect = false;
        private string bearerToken = "";
        private HttpContent content = null;
        private HttpMethod method = null;
        private string requestUri = "";
        private TimeSpan timeout = new TimeSpan(0, 0, 15);

        public HttpRequestBuilder AddAcceptHeader(string acceptHeader)
        {
            this.acceptHeader = acceptHeader;
            return this;
        }

        public HttpRequestBuilder AddAllowAutoRedirect(bool allowAutoRedirect)
        {
            this.allowAutoRedirect = allowAutoRedirect;
            return this;
        }

        public HttpRequestBuilder AddBearerToken(string bearerToken)
        {
            this.bearerToken = bearerToken;
            return this;
        }

        public HttpRequestBuilder AddContent(HttpContent content)
        {
            this.content = content;
            return this;
        }

        public HttpRequestBuilder AddMethod(HttpMethod method)
        {
            this.method = method;
            return this;
        }

        public HttpRequestBuilder AddRequestUri(string requestUri)
        {
            this.requestUri = requestUri;
            return this;
        }

        public HttpRequestBuilder AddTimeout(TimeSpan timeout)
        {
            this.timeout = timeout;
            return this;
        }

        public async Task<HttpResponseMessage> SendAsync()
        {
            // Check required arguments
            EnsureArguments();

            // Set up request
            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(requestUri)
            };

            if (content != null)
            {
                request.Content = content;
            }

            if (!string.IsNullOrEmpty(bearerToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            }

            request.Headers.Accept.Clear();
            if (!string.IsNullOrEmpty(acceptHeader))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptHeader));
            }

            // Setup client
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = allowAutoRedirect
            };

            var client = new System.Net.Http.HttpClient(handler)
            {
                Timeout = timeout
            };

            return await client.SendAsync(request);
        }

        #region " Private "

        private void EnsureArguments()
        {
            if (method == null)
            {
                throw new ArgumentNullException("Method");
            }

            if (string.IsNullOrEmpty(requestUri))
            {
                throw new ArgumentNullException("Request Uri");
            }
        }

        #endregion " Private "
    }
}
