using Newtonsoft.Json;

namespace ArmaPresetCreator.Web.Models.DTO
{
    public abstract class SteamBaseItemDto
    {
        [JsonProperty("publishedfileid")]
        public long PublishedFileId { get; set; }
        
        [JsonProperty("file_type")]
        public long FileType { get; set; }
    }
}
