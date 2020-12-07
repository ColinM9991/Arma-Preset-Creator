using System;
using System.Linq;
using ArmaPresetCreator.Web.Models;
using AutoMapper;

namespace ArmaPresetCreator.Web.Services
{
    public class ArmaPresetRequestCreator : IArmaPresetRequestCreator
    {
        private readonly IMapper mapper;
        public ArmaPresetRequestCreator(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ArmaPresetRequest Create(SteamWorkshopItem steamWorkshopItem)
        {
            if(steamWorkshopItem?.Items == null || steamWorkshopItem.Items?.Length == 0)
            {
                throw new UnsupportedWorkshopItemException();
            }

            var workshopItems = steamWorkshopItem.Items.ToList();
            if(steamWorkshopItem.IsAddon())
            {
                workshopItems.Insert(0, steamWorkshopItem);
            }

            var addons = workshopItems.Select(p => mapper.Map<ArmaAddon>(p));
            var armaPresetRequest = mapper.Map<ArmaPresetRequest>(steamWorkshopItem);
            armaPresetRequest.Items = addons.ToArray();

            return armaPresetRequest;
        }
    }
}