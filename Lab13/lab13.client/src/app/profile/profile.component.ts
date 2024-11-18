import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  username: string = '';
  name: string = '';
  phone_number: string = '';
  email: string = '';
  error: string = '';

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.loadProfile();
  }

  loadProfile(): void {
    this.http.get('http://localhost:5278/api/Home/GetProfile', {
      headers: {
        'Authorization': `Bearer ${this.getToken()}`
      }
    }).subscribe(
      (response: any) => {
        this.username = response.username;
        this.name = response.name;
        this.phone_number = response.phone_number;
        this.email = response.email;
      },
      error => {
        this.error = `Failed to fetch profile: ${error.message}`;
      }
    );
  }

  getToken(): string {
    const name = 'AuthToken=';
    const decodedCookie = decodeURIComponent(document.cookie);
    const cookies = decodedCookie.split(';');
    for (let i = 0; i < cookies.length; i++) {
      let c = cookies[i].trim();
      if (c.indexOf(name) === 0) {
        return c.substring(name.length, c.length);
      }
    }
    return '';
  }
}
