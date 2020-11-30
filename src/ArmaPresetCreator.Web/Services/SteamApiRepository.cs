using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmaPresetCreator.Web.Models;
using ArmaPresetCreator.Web.Models.DTO;
using AutoMapper;

namespace ArmaPresetCreator.Web.Services
{
    /// <inheritdoc />
    public class SteamApiRepository : ISteamApiRepository
    {
        private readonly ISteamApiService steamApiService;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of this type.
        /// </summary>
        /// <param name="steamApiService">The <see cref="ISteamApiService"/> which is used to fulfill requests to the Steam Web API.</param>
        /// <param name="mapper"><see cref="IMapper"/> used to map entities to data transfer objects.</param>
        public SteamApiRepository(ISteamApiService steamApiService, IMapper mapper)
        {
            this.steamApiService = steamApiService;
            this.mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<SteamWorkshopItem> GetPublishedItemDetailsAsync(long workshopItemId)
        {
            var steamWorkshopPublishedItemDetails = await steamApiService.GetPublishedItemDetailsAsync(workshopItemId);
            if (steamWorkshopPublishedItemDetails.Response.IsInvalid())
                return null;

            var steamWorkshopPublishedFile = steamWorkshopPublishedItemDetails.Response.PublishedFilesDetails[0];
            var steamWorkshopItem = mapper.Map<SteamWorkshopItem>(steamWorkshopPublishedFile);

            if (steamWorkshopPublishedFile.NumChildren <= 0) return steamWorkshopItem;

            steamWorkshopItem.Items = await GetChildrenAndIncludeNested(steamWorkshopPublishedFile);
            return steamWorkshopItem;
        }

        private async Task<SteamWorkshopItem[]> GetChildrenAndIncludeNested(SteamPublishedFileDetailDto steamPublishedFileDetail)
        {
            var childrenDependencies = await GetAllDependenciesAsync(steamPublishedFileDetail);
            var steamWorkshopItems = childrenDependencies.Select(workshopItem => mapper.Map<SteamWorkshopItem>(workshopItem)).ToList();

            // Filter the Workshop Items to remove duplicate items where the same item exists, based on the PublishedFileId.
            return steamWorkshopItems.Distinct(new SteamWorkshopItemComparer()).ToArray();
        }

        /// <summary>
        /// Navigates the tree structure of a workshop item.
        /// </summary>
        /// <param name="detail">The Steam workshop item to walk, can be a tree or leaf node.</param>
        /// <returns>All workshop items as a flat structure.</returns>
        private async Task<IEnumerable<SteamPublishedFileDetailDto>> GetAllDependenciesAsync(params SteamPublishedFileDetailDto[] detail)
        {
            var publishedFileDetails = new List<SteamPublishedFileDetailDto>();

            // Select the PublishedFileId from all children items, and return if there are none.
            var workshopItemChildrenIds = detail.SelectMany(p => p.Children.Select(child => child.PublishedFileId)).ToArray();
            if (workshopItemChildrenIds.Length == 0)
                return publishedFileDetails;

            // Retrieve workshop details on all child items.
            var workshopItemChildren = await steamApiService.GetPublishedItemDetailsAsync(workshopItemChildrenIds);

            // See if child items have further nested child items
            var childPublishedFilesDetails = workshopItemChildren.Response.PublishedFilesDetails.ToList();
            if (childPublishedFilesDetails.Any(publishedFile => publishedFile.NumChildren > 0))
            {
                var subChildrenWithNestedChildren = childPublishedFilesDetails.Where(publishedFile => publishedFile.NumChildren > 0).ToArray();

                // Recursively retrieve details from nested child items and add to the childPublishedFilesDetails collection
                var nestedChildren = await GetAllDependenciesAsync(subChildrenWithNestedChildren);
                childPublishedFilesDetails.AddRange(nestedChildren);
            }

            // Flatten the structure by adding all contents of childPublishedFilesDetails to publishedFileDetails.
            publishedFileDetails.AddRange(childPublishedFilesDetails);

            var validWorkshopItems = publishedFileDetails.Where(child => child.Result == 1);
            return validWorkshopItems;
        }
    }
}