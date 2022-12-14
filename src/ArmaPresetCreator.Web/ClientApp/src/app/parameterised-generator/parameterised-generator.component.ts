import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
    activatedRoute: ActivatedRoute,
    router: Router) {
    activatedRoute.params.subscribe(params => {
      this.generated = false;
      const publishedItemId = params.publishedItemId;
      if (!Number(publishedItemId)) {
        toastrService.error('Invalid share link, redirecting home');
        router.navigate(['/']);
        return;
      }

      if (publishedItemId) {
        this.generatorService
          .generatePreset(publishedItemId)
          .then(() => this.generated = true)
          .catch((err) => {
            toastrService.error(err);
            this.generated = false;
            this.failed = true;
          });
      }
    });
  }
}
