import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-controlapi',
  templateUrl: './controlapi.component.html',
  styleUrls: ['./controlapi.component.css']
})
export class ControlapiComponent {
  apiVersion: string = 'v1';
  customerId: number = 0;
  day: number = 0;
  month: number = 0;
  year: number = 0;
  response: any = '';

  constructor(private http: HttpClient) { }

  getBookingInfo(): void {
    const requestData = {
      customerId: this.customerId,
      day: this.day,
      month: this.month,
      year: this.year
    };

    const url = `http://localhost:5178/api/${this.apiVersion}/search/GetBookingInformation`;

    this.http.post(url, requestData, { responseType: 'json' })
      .subscribe(
        (data) => {
          this.response = JSON.stringify(data, null, 2);
        },
        (error) => {
          this.response = `Error: ${error.message}`;
        }
      );
  }
}
