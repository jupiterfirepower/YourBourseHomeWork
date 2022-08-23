using Newtonsoft.Json;
using System.Text;

namespace YB.Todo.TestProject
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient httpClient, string requestUri, T data)
#pragma warning disable CS8604 // Possible null reference argument.
            => httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri) { Content = Serialize(data) });
#pragma warning restore CS8604 // Possible null reference argument.

        public static Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient httpClient, string requestUri, T data, CancellationToken cancellationToken)
#pragma warning disable CS8604 // Possible null reference argument.
            => httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri) { Content = Serialize(data) }, cancellationToken);
#pragma warning restore CS8604 // Possible null reference argument.

        public static Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient httpClient, Uri requestUri, T data)
#pragma warning disable CS8604 // Possible null reference argument.
            => httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri) { Content = Serialize(data) });
#pragma warning restore CS8604 // Possible null reference argument.

        public static Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient httpClient, Uri requestUri, T data, CancellationToken cancellationToken)
#pragma warning disable CS8604 // Possible null reference argument.
            => httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri) { Content = Serialize(data) }, cancellationToken);
#pragma warning restore CS8604 // Possible null reference argument.

        private static HttpContent Serialize(object data) => new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
    }
}
