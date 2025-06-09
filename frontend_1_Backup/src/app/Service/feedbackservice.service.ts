import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Ifeedbacks } from '../interfaces/ifeedbacks';

export interface Feedback {
  id?: number;
  coachId: number;
  comments: string;
  timestamp: Date;
}

@Injectable({
  providedIn: 'root',
})
export class FeedbackserviceService {
  private baseUrl = 'https://localhost:7148/api/AthleteFeedback';

  constructor(private http: HttpClient) {}

  private getAuthHeaders() {
    const token = localStorage.getItem('token');
    return {
      headers: new HttpHeaders({
        Authorization: `Bearer ${token}`,
        'Content-Type': 'application/json',
      }),
    };
  }

  // Create new feedback
  submitFeedback(feedback: Feedback): Observable<Feedback> {
    return this.http.post<Feedback>(
      this.baseUrl,
      feedback,
      this.getAuthHeaders()
    );
  }

  // Get feedback for a specific athlete
  getFeedbackByAthleteId(athleteId: number): Observable<Feedback[]> {
    return this.http.get<Feedback[]>(
      `${this.baseUrl}/athlete/${athleteId}`,
      this.getAuthHeaders()
    );
  }

  // Get feedback given by a specific coach (optional)
  getFeedbackByCoachId(coachId: number): Observable<Feedback[]> {
    return this.http.get<Feedback[]>(
      `${this.baseUrl}/coach/${coachId}`,
      this.getAuthHeaders()
    );
  }
  getAllfeedback(): Observable<Ifeedbacks[]> {
    return this.http.get<Ifeedbacks[]>(
      `https://localhost:7148/api/spservice/feedbackinfo`,
      this.getAuthHeaders()
    );
  }
}
