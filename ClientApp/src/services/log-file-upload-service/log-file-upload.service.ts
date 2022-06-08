import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { API_URL } from 'src/app/app.api';

@Injectable({
  providedIn: 'root'
})
export class LogFileUploadService {

  constructor(private http: HttpClient, @Inject(API_URL) private apiUrl: string) { }

  uploadFileToApi(file: FormData) {
    return this.http.post(this.apiUrl + "api/log-upload", file);
  }
}
