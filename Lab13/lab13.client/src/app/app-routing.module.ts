import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { ControlComponent } from './control/control.component';
import { Lab1Component } from './lab1/lab1.component';
import { Lab2Component } from './lab2/lab2.component';
import { Lab3Component } from './lab3/lab3.component';
import { ControlapiComponent } from './controlapi/controlapi.component';
import { ProfileComponent } from './profile/profile.component';
import { AuthGuard } from './auth.guard';

const routes: Routes = [
  { path: 'Login', component: LoginComponent, canActivate: [AuthGuard] },
  { path: 'Control', component: ControlComponent, canActivate: [AuthGuard] },
  { path: 'Lab1', component: Lab1Component, canActivate: [AuthGuard] },
  { path: 'Lab2', component: Lab2Component, canActivate: [AuthGuard] },
  { path: 'Lab3', component: Lab3Component, canActivate: [AuthGuard] },
  { path: 'ControlAPI', component: ControlapiComponent, canActivate: [AuthGuard] },
  { path: 'Profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: '/Login', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
