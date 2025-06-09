import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TrainingProgram, TrainingprogramsService } from '../Service/trainingprograms.service';
import { Trainingprograms } from '../interfaces/trainingprogram';
import { UserserviceService } from '../Service/userservice.service';
import { SharedSubscriptionService } from '../Service/shared-subscription.service';
//import { AthleteSubscriptionService } from '../Service/athlete-subscription.service';
//import { AthleteSubscription } from '../interfaces/athlete-subscription';

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
  selector: 'app-training-programs',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './training-programs.component.html',
  styleUrls: ['./training-programs.component.css'],
})
export class TrainingProgramsComponent {
  trainingPrograms: Trainingprograms[] = [];
  isLoading = true;
  error: string | null = null;
  message: string = '';

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
    private trainingService: TrainingprogramsService,
    private router: Router,
    private userServices: UserserviceService,
    private sharedService: SharedSubscriptionService,
  ) {}

  ngOnInit(): void {
    this.loadAthleteData();
    this.loadTrainingPrograms();
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

  private loadTrainingPrograms(): void {
    this.trainingService.getAllPrograms().subscribe({
      next: (data) => {
        this.trainingPrograms = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.error = 'Failed to load training programs';
        this.isLoading = false;
        console.error(err);
      },
    });
  }

  subscribeToProgram(program: Trainingprograms): void {
    if (!this.athlete?.id) {
      console.log(program)
      console.error('Athlete ID not found!');
      return;
    }

    

    /*this.trainingService.createSubscription(subscription).subscribe({
      next: () => {
        this.message = 'Subscription successful!';
        this.sharedService.sendProgram(program); // optional
        this.router.navigate(['/trainingSession']);
      },
      error: (err) => {
        console.error('Subscription failed', err);
        this.message = 'Failed to subscribe. Please try again.';
      },
    });*/
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
