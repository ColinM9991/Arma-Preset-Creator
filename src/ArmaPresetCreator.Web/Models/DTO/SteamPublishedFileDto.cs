using Newtonsoft.Json;

namespace ArmaPresetCreator.Web.Models.DTO
{
    public class SteamPublishedFileDto : SteamResponseBase
    {
        [JsonProperty("publishedfiledetails")]
        public SteamPublishedFileDetailDto[] PublishedFilesDetails { get; set; }
    }
}
