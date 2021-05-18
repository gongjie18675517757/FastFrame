using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Middleware
{
    public static class HttpExpandMethods
    {
        public static async Task WriteJsonAsync<T>(this HttpResponse httpResponse, T obj)
        {
            await httpResponse.WriteAsJsonAsync(obj, options: new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            });
        }
    }
}
