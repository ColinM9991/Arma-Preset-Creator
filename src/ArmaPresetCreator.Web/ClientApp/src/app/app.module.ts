import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {ParameterisedGeneratorComponent} from './parameterised-generator/parameterised-generator.component';
import {CollectionGeneratorComponent} from './collection-generator/collection-generator.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {HttpClientModule} from "@angular/common/http";
import {ToastrModule} from "ngx-toastr";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import { FileSizeComponent } from './file-size/file-size.component';
import { WorkshopCardComponent } from './@core/components/workshop-card/workshop-card.component';
import { InfoComponent } from './info/info.component';

@NgModule({
  declarations: [
    AppComponent,
    ParameterisedGeneratorComponent,
    CollectionGeneratorComponent,
    FileSizeComponent,
    WorkshopCardComponent,
    InfoComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
