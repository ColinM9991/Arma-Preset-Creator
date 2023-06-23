import {AfterViewInit, Component, Input, OnInit} from '@angular/core';
import {SteamWorkshopItem} from "../../models/steam-workshop-item";
import {AddonCacheService} from "../../services/addon-cache.service";

@Component({
  selector: 'app-workshop-card',
  templateUrl: './workshop-card.component.html',
  styleUrls: ['./workshop-card.component.scss']
})
export class WorkshopCardComponent {
  @Input() workshopItem!: SteamWorkshopItem;
  @Input() workshopCollectionId!: number;
  @Input() canBeToggled!: boolean;

  constructor(private addonCache: AddonCacheService) {
  }

  getLastUpdatedDate(workshopItem: SteamWorkshopItem): Date {
    const date = new Date(0);
    date.setUTCSeconds(workshopItem.timeUpdated);

    return date;
  }

  onAddonToggled(event: any) {
    if (!this.canBeToggled) {
      return;
    }

    if (event) {
      this.addonCache.addToCache(this.workshopCollectionId, this.workshopItem.publishedFileId);
    } else {
      this.addonCache.removeFromCache(this.workshopCollectionId, this.workshopItem.publishedFileId);
    }
  }
}
