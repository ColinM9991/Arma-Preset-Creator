using System;

namespace ArmaPresetCreator.Web.Models;

public class BatchSteamWorkshopRequest
{
    public long[] WorkshopItemIds { get; set; } = Array.Empty<long>();
}