using System.Net.Http;
using Newtonsoft.Json;

namespace demoWebReact.HttpHelpers
{
    public static class HttpResponseExtensions
    {
        public static string ContentAsJson(this HttpResponseMessage response)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.SerializeObject(data);
        }

        public static string ContentAsString(this HttpResponseMessage response) => response.Content.ReadAsStringAsync().Result;

        public static T ContentAsType<T>(this HttpResponseMessage response)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            return string.IsNullOrEmpty(data) ?
                            default(T) :
                            JsonConvert.DeserializeObject<T>(data);
        }
    }
}
