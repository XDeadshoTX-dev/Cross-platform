import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Lab1Component } from './lab1/lab1.component';
import { Lab2Component } from './lab2/lab2.component';
import { Lab3Component } from './lab3/lab3.component';

const routes: Routes = [
  { path: 'Lab1', component: Lab1Component },
  { path: 'Lab2', component: Lab2Component },
  { path: 'Lab3', component: Lab3Component },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
