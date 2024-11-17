import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';

  constructor(private http: HttpClient) { }

  onSubmitLogin() {
    const loginData = { username: this.username, password: this.password };
    console.log('Login data', loginData);
    this.http.post('http://localhost:5278/api/Home/LoginAuth0', loginData)
      .subscribe(
        response => {
          console.log('Login successful', response);
        },
        error => {
          console.error('Login failed', error);
        }
      );
  }
}
