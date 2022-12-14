import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { GeneratorService } from '../generator.service';
import { ToastrService } from 'ngx-toastr';
import { Clipboard } from '@angular/cdk/clipboard';

@Component({
  selector: 'app-generator',
  templateUrl: './generator.component.html',
  styleUrls: ['./generator.component.scss']
})
export class GeneratorComponent implements OnInit {

  hyperlinkGenerated: boolean;
  hyperlink: string;

  form: FormGroup;
  isGenerating: boolean;

  constructor(
    private generatorService: GeneratorService,
    private formBuilder: FormBuilder,
    private toastrService: ToastrService,
    private clipboardService: Clipboard) { }

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
    this.generatorService.generatePreset(publishedItemId)
      .then(() => this.toastrService.success('Downloading Preset'))
      .catch(err => this.toastrService.error(err))
      .finally(() => this.isGenerating = false);
  }

  createHyperlink() {
    const publishedItemId = this
      .form
      .get('publishedItemId')
      .value;

    this.hyperlinkGenerated = true;
    this.hyperlink = `https://armapresetcreator.com/${publishedItemId}`;
  }

  copyHyperlink() {
    this.clipboardService.copy(this.hyperlink);
    this.toastrService.success('Copied!');
  }
}
