using Newtonsoft.Json;

namespace ArmaPresetCreator.Web.Models.DTO
{
    public class SteamPublishedFileDto : SteamResponseBase
    {
        [JsonProperty("publishedfiledetails")]
        public SteamPublishedFileDetailDto[] PublishedFilesDetails { get; set; }

        /// <summary>
        /// Determines whether the item is valid or not based on whether the app name is populated.
        /// </summary>
        /// <remarks>
        /// Steam returns a 200 response even if the workshop item is invalid.
        /// </remarks>
        /// <returns>True if the item is valid.</returns>
        public bool IsInvalid() => string.IsNullOrWhiteSpace(PublishedFilesDetails[0].AppName);
    }
}
