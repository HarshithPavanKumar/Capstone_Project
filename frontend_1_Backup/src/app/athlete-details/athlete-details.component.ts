import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
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
  selector: 'app-athlete-details',
  standalone: true,
  imports: [RouterModule, CommonModule, FormsModule],
  templateUrl: './athlete-details.component.html',
  styleUrls: ['./athlete-details.component.css'],
})
export class AthleteDetailsComponent {
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
    private router: Router,
    private userServices: UserserviceService
  ) {}

  ngOnInit(): void {
    const email = localStorage.getItem('Email');
    if (email) {
      this.userServices.getUserByEmail(email).subscribe({
        next: (user) => {
          this.athlete = user;
        },
        error: (err) => {
          console.error('Failed to fetch details', err);
        },
      });
    }
  }

  toggleEdit(): void {
    this.isEditing = !this.isEditing;
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
}
