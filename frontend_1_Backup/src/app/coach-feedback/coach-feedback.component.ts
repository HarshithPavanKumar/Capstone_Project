import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule, Router } from '@angular/router';
import { Ifeedbacks } from '../interfaces/ifeedbacks';
import { FeedbackserviceService } from '../Service/feedbackservice.service';
import { UserserviceService } from '../Service/userservice.service';
import { FormsModule } from '@angular/forms';

export interface Athlete {
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
  selector: 'app-coach-feedback',
  standalone: true,
  imports: [RouterModule, CommonModule, FormsModule],
  templateUrl: './coach-feedback.component.html',
  styleUrls: ['./coach-feedback.component.css'],
})
export class CoachFeedbackComponent {
  // Feedback related properties
  trainingPrograms: Ifeedbacks[] = [];
  isLoading = true;
  error: string | null = null;

  // Athlete details properties
  showDetails = false;
  isEditing = false;
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

  constructor(
    private feedbackService: FeedbackserviceService,
    private router: Router,
    private userServices: UserserviceService
  ) {}

  ngOnInit(): void {
    this.loadAthleteData();
    this.loadFeedbackData();
  }

  private loadAthleteData(): void {
    const email = localStorage.getItem('Email');
    if (email) {
      this.userServices.getUserByEmail(email).subscribe({
        next: (user) => {
          this.athlete = user;
        },
        error: (err) => {
          console.error('Failed to fetch athlete details', err);
        },
      });
    }
  }

  private loadFeedbackData(): void {
    this.feedbackService.getAllfeedback().subscribe({
      next: (data) => {
        this.trainingPrograms = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.error = 'Failed to load feedback data';
        this.isLoading = false;
        console.error(err);
      },
    });
  }

  // Methods to show or edit athlete details
  toggleDetails(): void {
    this.showDetails = !this.showDetails;
  }

  toggleEdit(): void {
    this.isEditing = !this.isEditing;
  }

  updateAthlete(): void {
    if (!this.athlete.email) return;

    this.userServices.updateAthlete(this.athlete).subscribe({
      next: (updated) => {
        this.athlete = updated;
        this.isEditing = false;
        alert('Athlete updated successfully!');
      },
      error: (err) => {
        console.error('Update failed', err);
        alert('Update failed.');
      },
    });
  }

  // Router navigation helper
  navigateTo(destination: string): void {
    this.router.navigate(['/', destination]);
  }

  // Go back to previous page
  goBack(): void {
    window.history.back();
  }

  // Logout and redirect to homepage
  logout(): void {
    localStorage.clear();
    this.router.navigate(['/home']);
  }

  // TrackBy function for ngFor
  trackById(index: number, item: Ifeedbacks): number {
    return item.id!;
  }
}
