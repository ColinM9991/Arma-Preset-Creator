using Newtonsoft.Json;
namespace ArmaPresetCreator.Web.Models.DTO
{
    public abstract class SteamResponseBase
    {
        [JsonProperty("result")]
        public int Result { get; set; }

        [JsonProperty("resultcount")]
        public int ResultCount { get; set; }
    }
}
