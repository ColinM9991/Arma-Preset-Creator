import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { saveAs } from 'file-saver';

@Injectable({
  providedIn: 'root'
})
export class GeneratorService {

  constructor(
    private httpClient: HttpClient
  ) { }

  public async generatePreset(publishedItemId: number) {
    const res = await this.httpClient.get(`/api/steam/workshop/publisheditems/${publishedItemId}`).toPromise();
    const presetRes = await this.httpClient.post(`/api/arma/preset/generate`, res, { responseType: 'blob' }).toPromise();
    saveAs(presetRes, `${res["name"]}.html`);
  }
}
