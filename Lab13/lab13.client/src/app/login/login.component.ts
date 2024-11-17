import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  fullname: string = '';
  password: string = '';
  passwordConfirm: string = '';
  phone: string = '';
  email: string = '';

  constructor(private http: HttpClient, private router: Router) { }

  onSubmitLogin() {
    const loginData = { username: this.username, password: this.password };
    this.http.post('http://localhost:5278/api/Home/LoginAuth0', loginData)
      .subscribe(
        response => {
          const responseString = JSON.stringify(response);
          const parsedResponse = JSON.parse(responseString);
          this.setCookie('AuthToken', parsedResponse.token, 1);
          this.router.navigate(['/Control']);
        },
        error => {
          console.error('Login failed', error);
        }
      );
  }
  onSubmitRegister() {
    const registerData = { username: this.username, fullname: this.fullname, password: this.password, passwordConfirm: this.passwordConfirm, phone: this.phone, email: this.email };
    this.http.post('http://localhost:5278/api/Home/RegistrationAuth0', registerData)
      .subscribe(
        response => {
          console.log(response);
        },
        error => {
          console.error('Login failed', error);
        }
      );
  }
  setCookie(name: string, value: string, hours: number) {
    const date = new Date();
    date.setTime(date.getTime() + (hours * 60 * 60 * 1000));
    const expires = `expires=${date.toUTCString()}`;
    const secure = 'secure';
    const sameSite = 'SameSite=Strict';
    document.cookie = `${name}=${value}; ${expires}; path=/; ${secure}; ${sameSite}`;
  }
}
