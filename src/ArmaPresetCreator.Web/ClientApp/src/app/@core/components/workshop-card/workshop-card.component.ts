import {Component, Input} from '@angular/core';
import {SteamWorkshopItem} from "../../models/steam-workshop-item";

@Component({
  selector: 'app-workshop-card',
  templateUrl: './workshop-card.component.html',
  styleUrls: ['./workshop-card.component.scss']
})
export class WorkshopCardComponent {
  @Input() workshopItem!: SteamWorkshopItem;
  @Input() workshopCollectionId!: number;
  @Input() canBeToggled!: boolean;

  getLastUpdatedDate(workshopItem: SteamWorkshopItem): Date {
    const date = new Date(0);
    date.setUTCSeconds(workshopItem.timeUpdated);

    return date;
  }
}
