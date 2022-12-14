import {SteamWorkshopItem} from "./steam-workshop-item";

export interface SteamWorkshopCollection extends SteamWorkshopItem {
  items: SteamWorkshopItem[];

  optional: boolean;
}

