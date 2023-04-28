import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";
import {WorkshopApiService} from "../@core/services/workshop-api.service";
import {ArmaPresetApiService} from "../@core/services/arma-preset-api.service";
import {PublishedItem} from "../collection-generator/collection-generator.component";
import {SteamWorkshopCollection} from "../@core/models/steam-workshop-collection";
import {SteamWorkshopItem} from "../@core/models/steam-workshop-item";
import {saveAs} from "file-saver";
import {combineLatest, map} from "rxjs";
import {ArmaAddon, ArmaPresetRequest} from "../@core/models/arma-preset-request";
import {AddonCacheService} from "../@core/services/addon-cache.service";

@Component({
  selector: 'app-parameterised-generator',
  templateUrl: './parameterised-generator.component.html',
  styleUrls: ['./parameterised-generator.component.scss']
})
export class ParameterisedGeneratorComponent implements OnInit {

  presetName: (string | null) = null;
  retrievedData: boolean = false;
  isGeneratingPreset: boolean = false;

  workshopCollections: SteamWorkshopCollection[] = [];

  constructor(
    private workshopApi: WorkshopApiService,
    private presetApi: ArmaPresetApiService,
    private toastrService: ToastrService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private addonCache: AddonCacheService) {
  }

  async ngOnInit() {
    combineLatest([this.activatedRoute.paramMap, this.activatedRoute.queryParamMap])
      .pipe(map(result => {
        const params = result[0];
        const queryParams = result[1];

        let collectionIds = params.get('publishedItemId')!.split(',').map((x: string) => new PublishedItem(Number(x), false));

        if (queryParams.has('optional'))
          collectionIds = collectionIds.concat(queryParams.get('optional')!.split(',').map((x: string) => new PublishedItem(Number(x), true)));

        if (queryParams.has('presetName'))
          this.presetName = queryParams.get('presetName');

        collectionIds = collectionIds.filter((value, index, self) => index === self.findIndex((t) => (t.publishedItemId === value.publishedItemId)))

        return collectionIds;
      }))
      .subscribe(async publishedItems => {
        if (publishedItems.length === 0) {
          await this.router.navigate(['/']);
        }

        this.workshopCollections = await this.getWorkshopItemsFromIds(publishedItems);
      });
  }

  private async getWorkshopItemsFromIds(publishedItems: PublishedItem[]): Promise<SteamWorkshopCollection[]> {
    let workshopCollections: SteamWorkshopCollection[] = [];
    try {
      const steamApiResult = await Promise.all<SteamWorkshopCollection>(publishedItems.map(i => this.workshopApi.getSteamCollection(i.publishedItemId)));

      workshopCollections = steamApiResult.flatMap(collection => {
        const publishedItem = publishedItems.find(x => x.publishedItemId == collection.publishedFileId)!;

        collection.optional = publishedItem.optional;
        collection.items.forEach(i => {
          if (!publishedItem.optional) {
            i.enabled = true;
            return;
          }

          i.enabled = this.addonCache.isInCache(collection.publishedFileId, i.publishedFileId);
        });

        return collection;
      });

      workshopCollections.forEach(collection => {
        if (!collection.optional)
          return;

        let collectionItemsFlat = workshopCollections.filter(c => c !== collection).flatMap(c => c.items);
        collection.items = collection.items.filter(p => !collectionItemsFlat.some(c => p.publishedFileId === c.publishedFileId));
      })
    } catch {
      this.toastrService.error('Error occurred generating preset');
      await this.router.navigate(['/']);
    }

    this.retrievedData = true;
    return workshopCollections;
  }

  async downloadPreset(): Promise<void> {
    this.isGeneratingPreset = true;
    this.toastrService.info("Downloading preset");

    const armaPresetRequest = this.getArmaPresetRequest;

    const resultBlob = await this.presetApi.generatePreset(armaPresetRequest);
    saveAs(resultBlob, `${decodeURIComponent(armaPresetRequest.name)}.html`);

    this.isGeneratingPreset = false;
    this.toastrService.success("Preset downloaded successfully");
  }

  getSortedWorkshopItems(workshopCollection: SteamWorkshopCollection): SteamWorkshopItem[] {
    return workshopCollection.items.sort((first, second) => first.name.localeCompare(second.name));
  }

  getEnabledItems(workshopCollection: SteamWorkshopCollection): SteamWorkshopItem[] {
    return workshopCollection.items.filter(x => x.enabled)
  }

  getNumberOfEnabledItems(workshopCollection: SteamWorkshopCollection): number {
    return workshopCollection.items.filter(x => x.enabled).length;
  }

  getSizeOfEnabledItems(workshopCollection: SteamWorkshopCollection): number {
    return this.getEnabledItems(workshopCollection).reduce((sum, current) => sum + current.fileSize, 0);
  }

  get getTotalSize(): number {
    return this.workshopCollections.flatMap(c => c.items.filter(x => x.enabled)).reduce((sum, current) => sum + current.fileSize, 0);
  }

  get getPresetName(): string {
    return this.presetName ?? this.workshopCollections[0].name;
  }

  get getArmaPresetRequest(): ArmaPresetRequest {
    let workshopItems = [...new Map(this.workshopCollections.flatMap(c => c.items).filter(x => x.enabled).map((item) => [item.publishedFileId, item])).values()];

    return {
      name: this.getPresetName,
      items: workshopItems.map(i => <ArmaAddon>{
        name: i.name,
        url: i.url,
      })
    };
  }

  toggleItems(event: Event, workshopCollection: SteamWorkshopCollection) {
    const checkbox = event.target as any;

    let toggleStatus: boolean = checkbox.indeterminate
      ? true
      : checkbox.checked;

    workshopCollection.items.forEach(i => {
      i.enabled = toggleStatus

      if (toggleStatus) {
        this.addonCache.addToCache(workshopCollection.publishedFileId, i.publishedFileId);
      } else {
        this.addonCache.removeFromCache(workshopCollection.publishedFileId, i.publishedFileId);
      }
    });
  }
}
