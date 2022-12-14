import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { saveAs } from 'file-saver';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  isGenerating: boolean;
  constructor(
    private httpClient: HttpClient,
    private activatedRoute: ActivatedRoute) {
    activatedRoute.queryParams.subscribe(params => {
      const publishedItemId = params["publishedItemId"];
      if (publishedItemId) {
        this.generatePreset(publishedItemId);
      }
    });
  }

  generatePreset(publishedItemId: number) {
    this.isGenerating = true;
    this.httpClient.get(`/api/steam/workshop/publisheditems/${publishedItemId}`)
      .subscribe(
        res => this.httpClient.post(`/api/arma/preset/generate`, res, { responseType: 'blob' })
          .subscribe(
            presetRes => {
              saveAs(presetRes, `${res["name"]}.html`);
              this.isGenerating = false;
            },
            error => console.log(error)),
        error => console.log(error));
  }
}
