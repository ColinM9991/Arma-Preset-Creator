using ArmaPresetCreator.Web.Models;

namespace ArmaPresetCreator.Web.Services
{
    public interface IArmaPresetRequestCreator
    {
        ArmaPresetRequest Create(SteamWorkshopItem steamWorkshopItem);
    }
}
