using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArmaPresetCreator.Web.IntegrationTests
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAs<T>(this HttpContent httpContent)
        {
            var content = await httpContent.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
