using Newtonsoft.Json;

namespace ArmaPresetCreator.Web.Models.DTO
{
    public class SteamApiResponse<T> where T : class
    {
        [JsonProperty("response")]
        public T Response { get; set; }
    }
}
