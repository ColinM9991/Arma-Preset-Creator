import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {SteamWorkshopCollection} from "../models/steam-workshop-collection";
import {catchError, firstValueFrom, Observable, throwError} from "rxjs";
import {BatchSteamWorkshopRequest} from "../models/batch-steam-workshop-request";

@Injectable({
  providedIn: 'root'
})
export class WorkshopApiService {

  constructor(private httpClient: HttpClient) {
  }

  async getSteamCollections(publishedItemIds: number[]): Promise<SteamWorkshopCollection[]> {
    const request = this
      .httpClient
      .post<SteamWorkshopCollection[]>('/api/steam/workshop/publisheditems/batch', new BatchSteamWorkshopRequest(publishedItemIds))
      .pipe(catchError(() => throwError(() => 'Error occurred when generating preset.')));

    const result = await firstValueFrom(request);
    if (result.length == 0) {
      throw 'Workshop item has no dependencies.';
    }

    return result;
  }
}
