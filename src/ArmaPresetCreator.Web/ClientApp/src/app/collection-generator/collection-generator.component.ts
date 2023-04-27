import {Component} from '@angular/core';
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {WorkshopApiService} from "../@core/services/workshop-api.service";
import {ToastrService} from "ngx-toastr";
import {ArmaPresetApiService} from "../@core/services/arma-preset-api.service";
import {Clipboard} from '@angular/cdk/clipboard';
import {NavigationExtras, Router} from "@angular/router";

const MAX_ITEMS = 20;

export class PublishedItem {
  publishedItemId: number;
  optional: boolean;

  constructor(
    publishedItemId: number,
    optional: boolean
  ) {
    this.publishedItemId = publishedItemId;
    this.optional = optional;
  }
}

@Component({
  selector: 'app-collection-generator',
  templateUrl: './collection-generator.component.html',
  styleUrls: ['./collection-generator.component.scss']
})
export class CollectionGeneratorComponent {

  readonly MAX_ITEMS = MAX_ITEMS;
  isGenerating: boolean = false;
  form: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private workshopApi: WorkshopApiService,
    private presetApi: ArmaPresetApiService,
    private toastrService: ToastrService,
    private clipboard: Clipboard,
    private router: Router) {
    this.form = this.formBuilder.group({
      name: [null, []],
      collectionItems: this.formBuilder.array([
          this.createWorkshopItemForm()
        ]
      )
    });
  }

  get collectionItems(): FormArray {
    return this.form.get('collectionItems') as FormArray;
  }

  get getPresetName(): FormControl {
    return this.form.get('name') as FormControl;
  }

  get getPresetNameValue(): string {
    return this.getPresetName?.value;
  }

  get getWorkshopItems(): PublishedItem[] {
    return this.collectionItems.controls.map(item => {
      const publishedItemId = item.get('publishedItemId')?.value;
      const optional = item.get('optional')?.value ?? false;

      const id: number = Number(publishedItemId)
        ? publishedItemId
        : Number(new URL(publishedItemId).searchParams.get('id'));

      return new PublishedItem(id, Boolean(optional));
    });
  }

  get getDistinctCollectionIds(): [string, string] {
    const publishedItems = this.getWorkshopItems;
    const mandatoryItems = [...new Set(publishedItems.filter(x => !x.optional).map(x => x.publishedItemId))].join(',');
    const optionalItems = [...new Set(publishedItems.filter(x => x.optional).map(x => x.publishedItemId))].join(',');

    return [mandatoryItems, optionalItems];
  }

  createWorkshopItemForm(): FormGroup {
    return this.formBuilder.group({
        publishedItemId: [
          null,
          [
            Validators.required,
            Validators.pattern(/(^\d*$)|(^https:\/\/steamcommunity\.com\/sharedfiles\/filedetails\/\?id=\d*$)/)
          ]
        ],
        optional: [
          false, []
        ]
      }
    )
  }

  addWorkshopItem(): void {
    if (this.collectionItems.length >= MAX_ITEMS)
      return;

    this.collectionItems.push(this.createWorkshopItemForm());
  }

  removeWorkshopItem(index: number) {
    this.collectionItems.removeAt(index);
  }

  async generatePreset() {
    this.isGenerating = true;

    const [mandatoryItems, optionalItems] = this.getDistinctCollectionIds;

    let extras: NavigationExtras = {
      queryParams: {}
    };

    if (optionalItems.length > 0)
      extras.queryParams!['optional'] = optionalItems;

    const name: string = this.getPresetNameValue;
    if (name)
      extras.queryParams!['presetName'] = name;

    await this.router.navigate(['/', mandatoryItems], extras);

    this.isGenerating = false;
  }

  createHyperlink(): URL {
    const [mandatoryItems, optionalItems] = this.getDistinctCollectionIds;

    const hyperlink = new URL(
      encodeURIComponent(mandatoryItems),
      window.location.href);

    if (optionalItems)
      hyperlink.searchParams.append('optional', encodeURIComponent(optionalItems));

    const name: string = this.getPresetNameValue;
    if (name)
      hyperlink.searchParams.append('presetName', encodeURIComponent(name));

    return hyperlink;
  }

  shareLink() {
    const hyperlink = this.createHyperlink();
    this.clipboard.copy(hyperlink.toString());
    this.toastrService.success('Link copied to clipboard');
  }
}
