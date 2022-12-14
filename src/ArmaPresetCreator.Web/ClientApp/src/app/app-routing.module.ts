import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {ParameterisedGeneratorComponent} from "./parameterised-generator/parameterised-generator.component";
import {CollectionGeneratorComponent} from "./collection-generator/collection-generator.component";

const routes: Routes = [
  { path: ':publishedItemId', component: ParameterisedGeneratorComponent },
  { path: '', component: CollectionGeneratorComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
