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
  hyperlink: URL;

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
          Validators.pattern(/(^[0-9]*$)|(^https:\/\/steamcommunity\.com\/sharedfiles\/filedetails\/\?id=[0-9]*$)/)
        ]
      ]
    });
  }

  generatePreset() {
    const publishedItemId = this.getPublishedItemId();

    this.isGenerating = true;
    this.generatorService.generatePreset(publishedItemId)
      .then(() => this.toastrService.success('Downloading Preset'))
      .catch(err => this.toastrService.error(err))
      .finally(() => this.isGenerating = false);
  }

  createHyperlink() {
    const publishedItemId = this.getPublishedItemId();

    this.hyperlinkGenerated = true;
    this.hyperlink = new URL(publishedItemId, window.location.href);
  }

  copyHyperlink() {
    this.clipboardService.copy(this.hyperlink.toString());
    this.toastrService.success('Copied!');
  }

  getPublishedItemId() {
    const publishedItemId = this
      .form
      .get('publishedItemId')
      .value;

    if (Number(publishedItemId)) {
      return publishedItemId;
    }

    const urlFormat = new URL(publishedItemId);
    return urlFormat.searchParams.get('id');
  }
}
