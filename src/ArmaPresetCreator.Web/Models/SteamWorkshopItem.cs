using System;
using System.ComponentModel.DataAnnotations;

namespace ArmaPresetCreator.Web.Models
{
    /// <summary>
    /// Used to represent a Steam workshop item.
    /// </summary>
    public class SteamWorkshopItem
    {
        /// <summary>
        /// The published file ID
        /// </summary>
        [Required]
        public long PublishedFileId { get; set; }

        /// <summary>
        /// The name of the Steam collection.
        /// </summary>
        [Required]
        public string Name { get; set; }
		
        /// <summary>
        /// Used to determine whether the item is a mod, mission or collection.
        /// </summary>
        /// <remarks>Unlike <see cref="FileType"/>, this can differentiate between a mission with dependencies and a mod with dependencies.
        /// Flags:
        /// * 1536 = Mission/Scenario
        /// * 1552 = Collection
        /// * 5632 = Mod/Addon
        /// </remarks>
        [Required]
        public long Flags { get; set; }

        /// <summary>
        /// Used to determine if the item is a collection containing mods, or just a workshop item with dependencies.
        /// </summary>
        /// <remarks>
        /// File Types:
        /// * 0 = Content (Addon/Mission)
        /// * 2 = Collection
        /// </remarks>
        [Required]
        public int FileType { get; set; }

        /// <summary>
        /// The workshop item dependencies.
        /// </summary>
        public SteamWorkshopItem[] Items { get; set; }

        /// <summary>
        /// The URL which the published item can be browsed.
        /// </summary>
        [Required]
        public Uri Url => new Uri($"https://steamcommunity.com/sharedfiles/filedetails/?id={PublishedFileId}");
    }
}
