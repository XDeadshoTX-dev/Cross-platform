import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { ControlComponent } from './control/control.component';
import { Lab1Component } from './lab1/lab1.component';
import { Lab2Component } from './lab2/lab2.component';
import { Lab3Component } from './lab3/lab3.component';
import { ControlapiComponent } from './controlapi/controlapi.component';
import { ProfileComponent } from './profile/profile.component';

const routes: Routes = [
  { path: 'Login', component: LoginComponent },
  { path: 'Control', component: ControlComponent },
  { path: 'Lab1', component: Lab1Component },
  { path: 'Lab2', component: Lab2Component },
  { path: 'Lab3', component: Lab3Component },
  { path: 'ControlAPI', component: ControlapiComponent },
  { path: 'Profile', component: ProfileComponent },
  { path: '', redirectTo: '/Login', pathMatch: 'full' }
  /*{ path: '**', redirectTo: '/Login' }*/
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
