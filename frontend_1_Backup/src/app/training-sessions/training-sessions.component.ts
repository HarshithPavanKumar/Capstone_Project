import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { SharedSubscriptionService } from '../Service/shared-subscription.service';
import { Trainingprograms } from '../interfaces/trainingprogram';
import { Subscription } from 'rxjs';

interface Athlete {
  id?: number;
  name: string;
  email: string;
  password: string;
  age: number;
  gender: string;
  height: number;
  weight: number;
  contact: string;
}

@Component({
  selector: 'app-training-sessions',
  standalone: true,
  imports: [RouterModule, CommonModule, FormsModule],
  templateUrl: './training-sessions.component.html',
  styleUrl: './training-sessions.component.css',
})
export class TrainingSessionsComponent implements OnInit, OnDestroy {
  subscriptions: Subscription = new Subscription();
  successMessage: string | null = null;

  athlete: Athlete = {
    name: '',
    email: '',
    password: '',
    age: 0,
    gender: '',
    height: 0,
    weight: 0,
    contact: '',
  };

  trainingSessions: {
    athleteName: string;
    programId: number;
    sessionTime: Date;
    programDetails: Trainingprograms;
    editMode?: boolean;
  }[] = [];

  constructor(
    private router: Router,
    private sharedService: SharedSubscriptionService
  ) {}

  ngOnInit(): void {
    const name = localStorage.getItem('Name') || 'Unknown Athlete';

    this.subscriptions = this.sharedService.programs$.subscribe((programs) => {
      this.trainingSessions = programs.map((program) => ({
        athleteName: name,
        programId: program.id ?? 0,
        sessionTime: new Date(),
        programDetails: program,
        editMode: false,
      }));
    });
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  toggleEdit(session: any): void {
    session.editMode = true;
  }

  saveChanges(session: any): void {
    session.editMode = false;
    this.successMessage = 'Session details updated successfully!';
    setTimeout(() => (this.successMessage = null), 3000);
  }
 
  navigateTo(destination: string): void {
    this.router.navigate(['/', destination]);
  }

  goBack(): void {
    window.history.back();
  }

  logout(): void {
    localStorage.clear();
    this.router.navigate(['/home']);
  }
}
