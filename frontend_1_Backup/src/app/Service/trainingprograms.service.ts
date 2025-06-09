import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Trainingprograms } from '../interfaces/trainingprogram';

export interface TrainingProgram {
  id?: number;
  coachId: number;
  duration: string;
  description: string;
  intensity: string;
  createdBy: string;
  createdDateTime?: Date;
}

@Injectable({
  providedIn: 'root',
})
export class TrainingprogramsService {
  private baseUrl = 'https://localhost:7148/api/AthleteTrainingProgram';

  constructor(private http: HttpClient) {}

  private getAuthHeaders() {
    const token = localStorage.getItem('token');
    return {
      headers: new HttpHeaders({
        Authorization: `Bearer ${token}`,
      }),
    };
  }

  // Get all training programs
  getAllPrograms(): Observable<Trainingprograms[]> {
    return this.http.get<Trainingprograms[]>(
      `https://localhost:7148/api/spservice/trainingprogram`,
      this.getAuthHeaders()
    );
  }

  // Get a training program by ID
  getProgramById(id: number): Observable<TrainingProgram> {
    return this.http.get<TrainingProgram>(
      `${this.baseUrl}/${id}`,
      this.getAuthHeaders()
    );
  }

  // Add a new training program
  addProgram(program: TrainingProgram): Observable<TrainingProgram> {
    return this.http.post<TrainingProgram>(
      this.baseUrl,
      program,
      this.getAuthHeaders()
    );
  }
  createSubscription(
    subscription: TrainingProgram
  ): Observable<TrainingProgram> {
    return this.http.post<TrainingProgram>(
      `https://localhost:7148/api/AthleteSubscriptions`,
      subscription
    );
  }
}
