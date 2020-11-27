using Newtonsoft.Json;

namespace ArmaPresetCreator.Web.Models.DTO
{
    public class SteamTagDto
    {
        [JsonProperty("tag")]
        public string Tag { get; set; }
    }
}
