import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ParameterisedGeneratorComponent } from './parameterised-generator/parameterised-generator.component';
import { GeneratorComponent } from './generator/generator.component';
import { GeneratorService } from './generator.service';
import { CommonModule } from '@angular/common';
import { ClipboardModule } from '@angular/cdk/clipboard';
import { NotFoundComponent } from './not-found/not-found.component';

@NgModule({
  declarations: [
    AppComponent,
    ParameterisedGeneratorComponent,
    GeneratorComponent,
    NotFoundComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ClipboardModule,
    ToastrModule.forRoot()
  ],
  providers: [
    GeneratorService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
