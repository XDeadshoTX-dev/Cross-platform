import { Component } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-lab3',
  templateUrl: './lab3.component.html',
  styleUrl: './lab3.component.css'
})
export class Lab3Component {
  selectedFile: File | null = null;
  labName: string = 'Lab3';
  output: string = '';
  error: string = '';

  constructor(private http: HttpClient) { }

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file && file.name === 'INPUT.TXT') {
      this.selectedFile = file;
      this.error = '';
    } else {
      this.error = 'Будь ласка, виберіть файл з назвою INPUT.TXT';
    }
  }
  onSubmit(): void {
    if (!this.selectedFile) {
      this.error = 'Файл не завантажено!';
      return;
    }

    const formData = new FormData();
    formData.append('inputFile', this.selectedFile);
    formData.append('lab', this.labName);
    console.log(this.labName);

    this.http.post('http://localhost:5278/api/Home/StartLab', formData, { responseType: 'json' })
      .subscribe(
        (response: any) => {
          console.log(response);
          const responseString = JSON.stringify(response);
          const parsedResponse = JSON.parse(responseString);
          this.output = parsedResponse.message;
          this.error = '';
        },
        (error: HttpErrorResponse) => {
          this.error = `Помилка: ${error.error || error.message}`;
          this.output = '';
        }
      );
  }
}
