using System;
using Newtonsoft.Json;

namespace ArmaPresetCreator.Web.Models.DTO
{
    public class SteamPublishedFileDetailDto : SteamBaseItemDto
    {
        [JsonProperty("result")]
        public long Result { get; set; }

        [JsonProperty("creator")]
        public string Creator { get; set; }

        [JsonProperty("creator_appid")]
        public long CreatorAppId { get; set; }

        [JsonProperty("consumer_appid")]
        public long ConsumerAppId { get; set; }

        [JsonProperty("consumer_shortcutid")]
        public long ConsumerShortcutId { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("file_size")]
        public long FileSize { get; set; }

        [JsonProperty("preview_file_size")]
        public long PreviewFileSize { get; set; }

        [JsonProperty("file_url")]
        public Uri FileUrl { get; set; }

        [JsonProperty("preview_url")]
        public Uri PreviewUrl { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("hcontent_file")]
        public string HcontentFile { get; set; }

        [JsonProperty("hcontent_preview")]
        public string HcontentPreview { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("file_description")]
        public string FileDescription { get; set; }

        [JsonProperty("time_created")]
        public long TimeCreated { get; set; }

        [JsonProperty("time_updated")]
        public long TimeUpdated { get; set; }

        [JsonProperty("visibility")]
        public long Visibility { get; set; }

        [JsonProperty("flags")]
        public long Flags { get; set; }

        [JsonProperty("workshop_file")]
        public bool WorkshopFile { get; set; }

        [JsonProperty("workshop_accepted")]
        public bool WorkshopAccepted { get; set; }

        [JsonProperty("show_subscribe_all")]
        public bool ShowSubscribeAll { get; set; }

        [JsonProperty("num_comments_public")]
        public long NumCommentsPublic { get; set; }

        [JsonProperty("banned")]
        public bool Banned { get; set; }

        [JsonProperty("ban_reason")]
        public string BanReason { get; set; }

        [JsonProperty("banner")]
        public string Banner { get; set; }

        [JsonProperty("can_be_deleted")]
        public bool CanBeDeleted { get; set; }

        [JsonProperty("app_name")]
        public string AppName { get; set; }

        [JsonProperty("can_subscribe")]
        public bool CanSubscribe { get; set; }

        [JsonProperty("subscriptions")]
        public long Subscriptions { get; set; }

        [JsonProperty("favorited")]
        public long Favorited { get; set; }

        [JsonProperty("followers")]
        public long Followers { get; set; }

        [JsonProperty("lifetime_subscriptions")]
        public long LifetimeSubscriptions { get; set; }

        [JsonProperty("lifetime_favorited")]
        public long LifetimeFavorited { get; set; }

        [JsonProperty("lifetime_followers")]
        public long LifetimeFollowers { get; set; }

        [JsonProperty("lifetime_playtime")]
        public long LifetimePlaytime { get; set; }

        [JsonProperty("lifetime_playtime_sessions")]
        public long LifetimePlaytimeSessions { get; set; }

        [JsonProperty("views")]
        public long Views { get; set; }

        [JsonProperty("num_children")]
        public long NumChildren { get; set; }

        [JsonProperty("num_reports")]
        public long NumReports { get; set; }

        [JsonProperty("children")]
        public SteamWorkshopChildItemDto[] Children { get; set; }

        [JsonProperty("language")]
        public long Language { get; set; }

        [JsonProperty("maybe_inappropriate_sex")]
        public bool MaybeInappropriateSex { get; set; }

        [JsonProperty("maybe_inappropriate_violence")]
        public bool MaybeInappropriateViolence { get; set; }
    }
}
