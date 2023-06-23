using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArmaPresetCreator.Web.Models;
using Microsoft.Extensions.Logging;

namespace ArmaPresetCreator.Web.Services;

public class LoggingSteamApiRepository : ISteamApiRepository
{
    private readonly ISteamApiRepository steamApiRepository;
    private readonly ILogger<LoggingSteamApiRepository> logger;

    public LoggingSteamApiRepository(
        ISteamApiRepository steamApiRepository,
        ILogger<LoggingSteamApiRepository> logger)
    {
        this.steamApiRepository = steamApiRepository ?? throw new ArgumentNullException(nameof(steamApiRepository));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task<IEnumerable<SteamWorkshopItem>> GetPublishedItemsDetailsAsync(long[] publishedItemIds)
    {
        logger.LogInformation("Retrieving information for {@PublishedItemId}", publishedItemIds);

        return steamApiRepository.GetPublishedItemsDetailsAsync(publishedItemIds);
    }
}