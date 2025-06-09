import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Athlete } from '../athlete-details/athlete-details.component';
import { Coach } from '../interfaces/coach';

@Injectable({
  providedIn: 'root',
})
export class UserserviceService {
  private baseUrl = 'https://localhost:7148/api';

  constructor(private http: HttpClient) {}

  // 🔐 Attach JWT token to requests that require authorization
  private getAuthHeaders() {
    const token = localStorage.getItem('token');
    return {
      headers: new HttpHeaders({
        Authorization: `Bearer ${token ?? ''}`,
        'Content-Type': 'application/json',
      }),
    };
  }

  // ✅ 1. Get Athlete by Email
  getUserByEmail(email: string): Observable<Athlete> {
    return this.http.get<Athlete>(
      `${this.baseUrl}/Athletes/byEmail?email=${encodeURIComponent(email)}`,
      this.getAuthHeaders()
    );
  }

  // ✅ 2. Update Athlete Details
  updateAthlete(athlete: Athlete): Observable<Athlete> {
    if (!athlete.id) {
      throw new Error('Athlete ID is required for update');
    }
    return this.http.put<Athlete>(
      `${this.baseUrl}/Athletes/${athlete.id}`,
      athlete,
      this.getAuthHeaders()
    );
  }

  // ✅ 3. Register a New Athlete
  registerAthlete(athlete: Omit<Athlete, 'id'>): Observable<Athlete> {
    return this.http.post<Athlete>(`${this.baseUrl}/Athletes`, athlete);
  }

  // ✅ 4. Check if Email Already Exists (for validation)
  emailExists(email: string): Observable<boolean> {
    return this.http
      .get<{ exists: boolean }>(
        `${this.baseUrl}/Athletes/exists?email=${encodeURIComponent(email)}`
      )
      .pipe(map((res) => res.exists));
  }

  // ✅ 5. Get Athlete by ID (if needed)
  getUserById(id: number): Observable<Athlete> {
    return this.http.get<Athlete>(
      `${this.baseUrl}/Athletes/${id}`,
      this.getAuthHeaders()
    );
  }

  // ✅ 6. Delete an Athlete (if needed)
  deleteUser(id: number): Observable<void> {
    return this.http.delete<void>(
      `${this.baseUrl}/Athletes/${id}`,
      this.getAuthHeaders()
    );
  }

  // ✅ 7. Get Currently Logged-in User using localStorage
  getCurrentUser(): Observable<Athlete> {
    const email = localStorage.getItem('Email');
    if (!email) throw new Error('No email found in localStorage');
    return this.getUserByEmail(email);
  }

  // ✅ 8. Get Coach by ID
  getCoachById(id: number): Observable<Coach> {
    return this.http.get<Coach>(
      `${this.baseUrl}/Coach/${id}`,
      this.getAuthHeaders()
    );
  }
}
