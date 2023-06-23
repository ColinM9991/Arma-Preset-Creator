export class BatchSteamWorkshopRequest {
  public workshopItemIds: number[];

  constructor(workshopItemIds: number[]) {
    this.workshopItemIds = workshopItemIds;
  }
}
