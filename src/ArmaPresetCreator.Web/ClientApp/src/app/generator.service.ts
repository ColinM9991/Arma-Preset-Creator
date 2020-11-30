import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { saveAs } from 'file-saver';

@Injectable({
  providedIn: 'root'
})
export class GeneratorService {

  constructor(
    private httpClient: HttpClient,
    private toastrService: ToastrService
  ) { }

  public generatePreset(publishedItemId: number) {
    return this.httpClient.get(`/api/steam/workshop/publisheditems/${publishedItemId}`)
      .subscribe(
        res => this.httpClient.post(`/api/arma/preset/generate`, res, { responseType: 'blob' })
          .subscribe(
            presetRes => {
              saveAs(presetRes, `${res["name"]}.html`);
              this.toastrService.success('Downloading preset');
            },
            () => this.toastrService.error('Error occurred when generating preset, please ensure workshop item ID is correct.')),
        () => this.toastrService.error('Error occurred when retrieving Steam Workshop details, please ensure ID is correct.'));
  }
}
