import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { saveAs } from 'file-saver';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

export interface SteamWorkshopItem {
  publishedFileId: number;
  name: string;
  flags: number;
  fileType: number;
  items: SteamWorkshopItem[];
}

@Injectable({
  providedIn: 'root'
})
export class GeneratorService {

  constructor(
    private httpClient: HttpClient
  ) { }

  public async generatePreset(publishedItemId: number) {
    const res = await this
      .httpClient
      .get<SteamWorkshopItem>(`/api/steam/workshop/publisheditems/${publishedItemId}`)
      .pipe(catchError(() => throwError('Error occurred when generating preset.')))
      .toPromise();
    if (!res.items) {
      throw "Workshop item has no dependencies.";
    }

    const presetRes = await this
      .httpClient
      .post(`/api/arma/preset/generate`, res, { responseType: 'blob' })
      .pipe(catchError(() => throwError('Error occurred when generating preset.')))
      .toPromise();

    saveAs(presetRes, `${res["name"]}.html`);
  }
}
