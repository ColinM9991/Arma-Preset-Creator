using System.Threading.Tasks;
using ArmaPresetCreator.Web.Models;

namespace ArmaPresetCreator.Web.Services
{
    /// <summary>
    /// Provides a mechanism for retrieving details from a Steam Workshop item.
    /// </summary>
    public interface ISteamApiRepository
    {
        /// <summary>
        /// Retrieves the workshop item details, including children at all levels, and flattens, as well as filtering for duplicates, the resulting set.
        /// </summary>
        /// <param name="publishedItemId">The top-most published item ID of the workshop item to walk.</param>
        /// <returns><see cref="SteamWorkshopItem"/> which is essentially a flat structure containing all top level, and children nodes at all levels.</returns>
        Task<SteamWorkshopItem> GetPublishedItemDetailsAsync(long publishedItemId);
    }
}