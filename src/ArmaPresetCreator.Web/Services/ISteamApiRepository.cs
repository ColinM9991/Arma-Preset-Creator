using System.Collections.Generic;
using System.Threading.Tasks;
using ArmaPresetCreator.Web.Models;

namespace ArmaPresetCreator.Web.Services
{
    /// <summary>
    /// Provides a mechanism for retrieving details from a Steam Workshop item.
    /// </summary>
    public interface ISteamApiRepository
    {
        Task<IEnumerable<SteamWorkshopItem>> GetPublishedItemsDetailsAsync(long[] publishedItemIds);
    }
}