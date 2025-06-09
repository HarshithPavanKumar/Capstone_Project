import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Trainingprograms } from '../interfaces/trainingprogram';

@Injectable({
  providedIn: 'root',
})
export class SharedSubscriptionService {
  private programsSubject = new BehaviorSubject<Trainingprograms[]>([]);
  programs$ = this.programsSubject.asObservable();

  private programs: Trainingprograms[] = [];

  sendProgram(program: Trainingprograms) {
    const exists = this.programs.find(p => p.id === program.id);
    if (!exists) {
      this.programs.push(program);
      this.programsSubject.next(this.programs);
    }
  }

  clearPrograms() {
    this.programs = [];
    this.programsSubject.next([]);
  }
}
