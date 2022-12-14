import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GeneratorService } from '../generator.service';

@Component({
  selector: 'app-parameterised-generator',
  templateUrl: './parameterised-generator.component.html',
  styleUrls: ['./parameterised-generator.component.scss']
})
export class ParameterisedGeneratorComponent {

  generated: boolean;
  constructor(
    private generatorService: GeneratorService,
    activatedRoute: ActivatedRoute) {
    activatedRoute.params.subscribe(params => {
      this.generated = false;
      const publishedItemId = params.publishedItemId;
      if (publishedItemId) {
        this.generatorService.generatePreset(publishedItemId).add(() => this.generated = true);
      }
    });
  }
}
