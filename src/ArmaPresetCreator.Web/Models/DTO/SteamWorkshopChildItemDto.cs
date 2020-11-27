using Newtonsoft.Json;

namespace ArmaPresetCreator.Web.Models.DTO
{
    public class SteamWorkshopChildItemDto : SteamBaseItemDto
    {
        [JsonProperty("sortorder")]
        public long SortOrder { get; set; }
    }
}
