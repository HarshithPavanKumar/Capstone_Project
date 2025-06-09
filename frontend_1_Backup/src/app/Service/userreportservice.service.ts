import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Userinforeport } from '../interfaces/userinforeport';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserreportserviceService {
  constructor(private http: HttpClient) {}

  getInfo(): Observable<Userinforeport> {
    return this.http.get<Userinforeport>(
      `https://localhost:7148/api/Athletes/Reports`
    );
  }
}
