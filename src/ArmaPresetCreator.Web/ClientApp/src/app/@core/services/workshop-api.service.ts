import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {SteamWorkshopCollection} from "../models/steam-workshop-collection";
import {catchError, firstValueFrom, Observable, throwError} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class WorkshopApiService {

  constructor(private httpClient: HttpClient) {
  }

  async getSteamCollection(publishedItemId: number): Promise<SteamWorkshopCollection> {
    const request = this
      .httpClient
      .get<SteamWorkshopCollection>(`/api/steam/workshop/publisheditems/${publishedItemId}`)
      .pipe(catchError(() => throwError(() => 'Error occurred when generating preset.')))

    const result = await firstValueFrom(request);
    if (!result.items) {
      throw "Workshop item has no dependencies.";
    }

    return result;
  }
}
