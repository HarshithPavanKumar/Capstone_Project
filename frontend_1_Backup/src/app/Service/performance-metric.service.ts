import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Performance {
  id?: number;
  athleteId: number;
  averageSpeed: number;
  bestSpeed: number;
  distanceCovered: number;
  skillLevel: number;
  improvement: number;
  accuracy: number;
  caloriesBurned: number;
  duration: number;
  timestamp: Date;
}

@Injectable({
  providedIn: 'root',
})
export class PerformanceMetricService {
  private baseUrl = 'https://localhost:7148/api/PerformanceMetrics';

  constructor(private http: HttpClient) {}

  private getAuthHeaders() {
    const token = localStorage.getItem('token');
    return {
      headers: new HttpHeaders({
        Authorization: `Bearer ${token}`,
      }),
    };
  }

  // Add a new performance metric
  addPerformanceMetric(metric: Performance): Observable<Performance> {
    return this.http.post<Performance>(
      this.baseUrl,
      metric,
      this.getAuthHeaders()
    );
  }

  // Get all metrics for a given athlete
  getMetricsByAthleteId(athleteId: number): Observable<Performance[]> {
    return this.http.get<Performance[]>(
      `${this.baseUrl}/athlete/${athleteId}`,
      this.getAuthHeaders()
    );
  }

  // Update an existing performance metric
  updateMetric(metric: Performance): Observable<Performance> {
    if (!metric.id) {
      throw new Error('Metric ID is required for update.');
    }
    return this.http.put<Performance>(
      `${this.baseUrl}/${metric.id}`,
      metric,
      this.getAuthHeaders()
    );
  }
}
