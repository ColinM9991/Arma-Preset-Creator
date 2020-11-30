import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GeneratorComponent } from './generator/generator.component';
import { ParameterisedGeneratorComponent } from './parameterised-generator/parameterised-generator.component';

const routes: Routes = [
  {
    path: '',
    component: GeneratorComponent,
    pathMatch: 'full'
  },
  {
    path: ':publishedItemId',
    component: ParameterisedGeneratorComponent,
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
