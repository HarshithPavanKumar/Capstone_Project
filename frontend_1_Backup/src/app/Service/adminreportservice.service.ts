import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Admininforeport } from '../interfaces/userinforeport';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AdminreportserviceService {
  constructor(private http: HttpClient) {}

  getInfo(): Observable<Admininforeport> {
    return this.http.get<Admininforeport>(
      `https://localhost:7148/api/Coach/adminReports`
    );
  }
}
