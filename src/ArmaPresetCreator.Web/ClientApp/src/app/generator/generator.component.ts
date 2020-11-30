import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { GeneratorService } from '../generator.service';

@Component({
  selector: 'app-generator',
  templateUrl: './generator.component.html',
  styleUrls: ['./generator.component.scss']
})
export class GeneratorComponent implements OnInit {

  form: FormGroup;
  isGenerating: boolean;
  constructor(
    private generatorService: GeneratorService,
    private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      publishedItemId: [
        null,
        [
          Validators.required,
          Validators.maxLength(24),
          Validators.pattern('^[0-9]*$')
        ]
      ]
    });
  }

  generatePreset() {
    const publishedItemId = this
      .form
      .get('publishedItemId')
      .value;

    this.isGenerating = true;
    this.generatorService.generatePreset(publishedItemId).add(() => this.isGenerating = false);
  }
}
