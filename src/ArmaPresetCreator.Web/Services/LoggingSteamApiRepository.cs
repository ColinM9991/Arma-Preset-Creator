using System;
using System.Threading.Tasks;
using ArmaPresetCreator.Web.Models;
using Microsoft.Extensions.Logging;

namespace ArmaPresetCreator.Web.Services;

public class LoggingSteamApiRepository : ISteamApiRepository
{
    private readonly ISteamApiRepository steamApiRepository;
    private readonly ILogger<ISteamApiRepository> logger;

    public LoggingSteamApiRepository(
        ISteamApiRepository steamApiRepository,
        ILogger<ISteamApiRepository> logger)
    {
        this.steamApiRepository = steamApiRepository ?? throw new ArgumentNullException(nameof(steamApiRepository));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public Task<SteamWorkshopItem> GetPublishedItemDetailsAsync(long publishedItemId)
    {
        logger.LogInformation("Retrieving information for {PublishedItemId}", publishedItemId);

        return steamApiRepository.GetPublishedItemDetailsAsync(publishedItemId);
    }
}