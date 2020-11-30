import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GeneratorService } from '../generator.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-parameterised-generator',
  templateUrl: './parameterised-generator.component.html',
  styleUrls: ['./parameterised-generator.component.scss']
})
export class ParameterisedGeneratorComponent {

  generated: boolean;
  failed: boolean;
  constructor(
    private generatorService: GeneratorService,
    toastrService: ToastrService,
    activatedRoute: ActivatedRoute) {
    activatedRoute.params.subscribe(params => {
      this.generated = false;
      const publishedItemId = params.publishedItemId;
      if (publishedItemId) {
        this.generatorService
          .generatePreset(publishedItemId)
          .then(() => this.generated = true)
          .catch(() => {
            toastrService.error('Error occurred when generating preset.');
            this.generated = false;
            this.failed = true;
          });
      }
    });
  }
}
