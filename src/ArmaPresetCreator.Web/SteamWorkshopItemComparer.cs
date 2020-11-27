using ArmaPresetCreator.Web.Models;
using System.Collections.Generic;

namespace ArmaPresetCreator.Web
{
    public class SteamWorkshopItemComparer : IEqualityComparer<SteamWorkshopItem>
    {
        public bool Equals(SteamWorkshopItem x, SteamWorkshopItem y)
        {
            return x.PublishedFileId == y.PublishedFileId;
        }

        public int GetHashCode(SteamWorkshopItem obj)
        {
            return (int)obj.PublishedFileId;
        }
    }
}
