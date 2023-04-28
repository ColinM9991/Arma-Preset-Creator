import {Injectable, OnDestroy} from '@angular/core';

const CACHE_KEY = 'APC-cached-addons'

@Injectable({
  providedIn: 'root'
})
export class AddonCacheService implements OnDestroy {
  selectedAddonsCache: { [key: number]: number[] };

  constructor() {
    this.selectedAddonsCache = this.getCachedItems();
  }

  ngOnDestroy(): void {
  }

  addToCache(collectionId: number, workshopItem: number) {
    let cachedItems = this.selectedAddonsCache[collectionId];
    if (!cachedItems) {
      cachedItems = [];
    }

    if(cachedItems.includes(workshopItem)) {
      return;
    }

    cachedItems.push(workshopItem);

    this.selectedAddonsCache[collectionId] = cachedItems;
    localStorage.setItem(CACHE_KEY, JSON.stringify(this.selectedAddonsCache));
  }

  removeFromCache(collectionId: number, workshopItem: number) {
    let items = this.selectedAddonsCache[collectionId];
    if (!items) {
      return;
    }

    if (!items.includes(workshopItem)) {
      return;
    }

    this.selectedAddonsCache[collectionId] = items.filter(x => workshopItem !== x);
    localStorage.setItem(CACHE_KEY, JSON.stringify(this.selectedAddonsCache));
  }

  isInCache(collectionId: number, workshopItem: number): boolean {
    let item = this.selectedAddonsCache[collectionId];
    if(!item) {
      return false;
    }

    return item.includes(workshopItem);
  }

  private getCachedItems(): { [key: number]: number[] } {
    let cachedItems = localStorage.getItem(CACHE_KEY);
    if (!cachedItems) {
      return {};
    }

    return JSON.parse(cachedItems);
  }
}
