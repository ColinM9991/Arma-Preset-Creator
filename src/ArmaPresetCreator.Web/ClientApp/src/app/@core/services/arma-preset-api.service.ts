import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {firstValueFrom} from "rxjs";
import {ArmaPresetRequest} from "../models/arma-preset-request";

@Injectable({
  providedIn: 'root'
})
export class ArmaPresetApiService {

  constructor(private httpClient: HttpClient) {
  }

  async generatePreset(armaPresetRequest: ArmaPresetRequest): Promise<Blob> {
    const request = this
      .httpClient
      .post(`/api/arma/preset/generate`, armaPresetRequest, {responseType: 'blob'});

    return await firstValueFrom(request);
  }
}
