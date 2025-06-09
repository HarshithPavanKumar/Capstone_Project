import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Personalizedtraining } from '../interfaces/personalizedtraining';

@Injectable({
  providedIn: 'root',
})
export class PersonalizedtrainingprogramService {
  private url = 'https://localhost:7148/api/PersonalizedTrainingProgram';
  constructor(private http: HttpClient) {}
  private getAuthHeaders() {
    const token = localStorage.getItem('token');
    return {
      headers: new HttpHeaders({
        Authorization: `Bearer ${token}`,
      }),
    };
  }
  getProgramById(id: number): Observable<Personalizedtraining> {
    return this.http.get<Personalizedtraining>(
      `${this.url}/${id}`,
      this.getAuthHeaders()
    );
  }
} 
