using ArmaPresetCreator.Web.Models;
using AutoMapper;
using System.Collections.Generic;

namespace ArmaPresetCreator.Web.Services
{
    public interface IArmaPresetRequestCreator
    {
        ArmaPresetRequest Create(SteamWorkshopItem steamWorkshopItem);
    }

    public class ArmaPresetRequestCreator : IArmaPresetRequestCreator
    {
        private readonly IMapper mapper;
        public ArmaPresetRequestCreator(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ArmaPresetRequest Create(SteamWorkshopItem steamWorkshopItem)
        {
            if(steamWorkshopItem?.Items.Length == 0)
            {
                throw new UnsupportedWorkshopItemException();
            }

            var armaPresetRequest = mapper.Map<ArmaPresetRequest>(steamWorkshopItem);

            var addons = new List<ArmaAddon>();
            if(steamWorkshopItem.FileType == 0 && steamWorkshopItem.Flags == 5632)
            {
                AddWorkshopItem(steamWorkshopItem, addons);
            }

            foreach(var workshopItem in steamWorkshopItem.Items)
            {
                AddWorkshopItem(workshopItem, addons);
            }

            armaPresetRequest.Items = addons.ToArray();

            return armaPresetRequest;
        }

        private  void AddWorkshopItem(SteamWorkshopItem steamWorkshopItem, List<ArmaAddon> addons)
        {
            var addon = mapper.Map<ArmaAddon>(steamWorkshopItem);
            addons.Add(addon);
        }
    }
}
