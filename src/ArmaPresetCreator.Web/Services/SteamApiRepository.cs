using System;
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
        private static readonly Task<SteamWorkshopItem[]> EmptyEnumerableTask = Task.FromResult(Array.Empty<SteamWorkshopItem>());
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

        public async Task<IEnumerable<SteamWorkshopItem>> GetPublishedItemsDetailsAsync(long[] publishedItemIds)
        {
            var steamApiResponse = await steamApiService.GetPublishedItemDetailsAsync(publishedItemIds);

            var steamWorkshopItems = steamApiResponse.Response.PublishedFilesDetails;
            if (!steamWorkshopItems.Any()) return Enumerable.Empty<SteamWorkshopItem>();

            var batchJobs = steamWorkshopItems.ToDictionary(x => x,
                x => x.NumChildren > 0
                    ? GetChildrenAndIncludeNested(x)
                    : EmptyEnumerableTask);

            await Task.WhenAll(batchJobs.Values);
            
            var workshopItems = new List<SteamWorkshopItem>();
            foreach (var item in steamWorkshopItems)
            {
                var mappedItem = mapper.Map<SteamWorkshopItem>(item);
                mappedItem.Items = await batchJobs[item];

                workshopItems.Add(mappedItem);
            }

            return workshopItems;
        }

        private async Task<SteamWorkshopItem[]> GetChildrenAndIncludeNested(
            SteamPublishedFileDetailDto steamPublishedFileDetail)
        {
            var childrenDependencies = await GetAllDependenciesAsync(new[] {steamPublishedFileDetail.PublishedFileId},
                steamPublishedFileDetail);
            var steamWorkshopItems = childrenDependencies
                .Select(workshopItem => mapper.Map<SteamWorkshopItem>(workshopItem)).ToList();

            // Filter the Workshop Items to remove duplicate items where the same item exists, based on the PublishedFileId.
            return steamWorkshopItems.Distinct(new SteamWorkshopItemComparer()).Where(x => x.IsAddon()).ToArray();
        }

        /// <summary>
        /// Navigates the tree structure of a workshop item.
        /// </summary>
        /// <param name="detail">The Steam workshop item to walk, can be a tree or leaf node.</param>
        /// <returns>All workshop items as a flat structure.</returns>
        private async Task<IEnumerable<SteamPublishedFileDetailDto>> GetAllDependenciesAsync(long[] existingIds,
            params SteamPublishedFileDetailDto[] detail)
        {
            var publishedFileDetails = new List<SteamPublishedFileDetailDto>();

            // Select the PublishedFileId from all children items, and return if there are none.
            var workshopItemChildrenIds = detail.SelectMany(p => p.Children.Select(child => child.PublishedFileId))
                .Where(id => !existingIds.Contains(id)).ToArray();
            if (workshopItemChildrenIds.Length == 0)
                return publishedFileDetails;

            // Retrieve workshop details on all child items.
            var workshopItemChildren = await steamApiService.GetPublishedItemDetailsAsync(workshopItemChildrenIds);

            // See if child items have further nested child items
            var childPublishedFilesDetails = workshopItemChildren.Response.PublishedFilesDetails.ToList();
            if (childPublishedFilesDetails.Any(publishedFile => publishedFile.NumChildren > 0))
            {
                var existingIdsWithSubChildrenIds = existingIds.Concat(workshopItemChildrenIds).ToArray();
                var subChildrenWithNestedChildren = childPublishedFilesDetails
                    .Where(publishedFile => publishedFile.NumChildren > 0).ToArray();

                // Recursively retrieve details from nested child items and add to the childPublishedFilesDetails collection
                var nestedChildren =
                    await GetAllDependenciesAsync(existingIdsWithSubChildrenIds, subChildrenWithNestedChildren);
                childPublishedFilesDetails.AddRange(nestedChildren);
            }

            // Flatten the structure by adding all contents of childPublishedFilesDetails to publishedFileDetails.
            publishedFileDetails.AddRange(childPublishedFilesDetails);

            var validWorkshopItems = publishedFileDetails.Where(child => child.Result == 1);
            return validWorkshopItems;
        }
    }
}