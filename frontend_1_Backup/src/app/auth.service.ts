import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable, tap } from 'rxjs';
import { CoachRegisterDto } from './interfaces/coachregisterdto';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl = 'https://localhost:7148/api/Athletes/login';
  private existsUrl = 'https://localhost:7148/api/Athletes/exists';
  private registerUrl = 'https://localhost:7148/api/Athletes';
  private adminLoginUrl = 'https://localhost:7148/api/Coach/coachlogin';
  private coachRegisterUrl = 'https://localhost:7148/api/Coach'; // ✅ Coach registration endpoint

  constructor(private http: HttpClient) {}

  // ✅ Athlete Login
  login(email: string, password: string): Observable<string> {
    return this.http
      .post(this.baseUrl, { email, password }, { responseType: 'text' })
      .pipe(
        tap((token: string) => {
          if (token) {
            localStorage.setItem('Email', email);
            localStorage.setItem('token', token);
          }
        })
      );
  }

  // ✅ Admin Login
  adminLogin(email: string, password: string): Observable<string> {
    return this.http
      .post(this.adminLoginUrl, { email, password }, { responseType: 'text' })
      .pipe(
        tap((token: string) => {
          if (token) {
            localStorage.setItem('AdminEmail', email);
            localStorage.setItem('admin-token', token);
          }
        })
      );
  }

  // ✅ Register Athlete (Full Model)
  registerAthlete(data: {
    name: string;
    age: number;
    gender: string;
    height: number;
    weight: number;
    contact: string;
    email: string;
    password: string;
  }): Observable<any> {
    return this.http.post(this.registerUrl, data);
  }

  // ✅ Register Coach (NEW)
  /*registerCoach(data: {
    name: string;
    email: string;
    password: string;
    specialty: string;
    experienceLevel: string;
    createdBy?: string;
  }): Observable<any> {
    return this.http.post(`https://localhost:7148/api/Coach`, data);
  }*/
  registerCoach(data: CoachRegisterDto): Observable<any> {
    return this.http.post(this.coachRegisterUrl, data);
  }

  // ✅ Get Athlete by Email
  getAthleteByEmail(email: string): Observable<any> {
    const url = `https://localhost:7148/api/Athletes/byemail?email=${encodeURIComponent(
      email
    )}`;
    return this.http.get<any>(url);
  }

  // ✅ Check if Email Exists
  emailExists(email: string): Observable<boolean> {
    return this.http
      .get<{ exists: boolean }>(
        `${this.existsUrl}?email=${encodeURIComponent(email)}`
      )
      .pipe(map((response) => response.exists));
  }

  // ✅ Logout
  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('Email');
    localStorage.removeItem('admin-token');
    localStorage.removeItem('AdminEmail');
  }

  // ✅ Get JWT Token
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // ✅ Check if Athlete is Logged In
  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  // ✅ Check if Admin is Logged In
  isAdminLoggedIn(): boolean {
    return !!localStorage.getItem('admin-token');
  }
}
