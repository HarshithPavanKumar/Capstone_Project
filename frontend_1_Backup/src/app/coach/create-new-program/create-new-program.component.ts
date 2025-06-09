import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { UserserviceService } from '../../Service/userservice.service';
import { Coach } from '../../interfaces/coach';

export interface TrainingProgram {
  name: string;
  description: string;
  duration: number;
  intensity: string;
}

@Component({
  selector: 'app-create-new-program',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './create-new-program.component.html',
  styleUrls: ['./create-new-program.component.css'],
})
export class CreateNewProgramComponent {
  tokenPayload: any = null;
  coachName: string = 'Admin';

  program: TrainingProgram = {
    name: '',
    description: '',
    duration: 0,
    intensity: '',
  };

  isSubmitted = false;

  constructor(
    private router: Router,
    private userService: UserserviceService
  ) {}

  ngOnInit(): void {
    const token = localStorage.getItem('admin-token');
    if (token) {
      this.tokenPayload = this.parseJwt(token);
      const adminId = this.tokenPayload?.UserId;
      if (adminId) {
        this.userService.getCoachById(adminId).subscribe({
          next: (coach: Coach) => {
            this.coachName = coach.name;
          },
          error: (err) => {
            console.error('Error fetching coach:', err);
          },
        });
      }
    }
  }

  // Add this method to decode the JWT
  parseJwt(token: string): any {
    try {
      return JSON.parse(atob(token.split('.')[1]));
    } catch (e) {
      console.error('Invalid token', e);
      return null;
    }
  }

  createProgram(): void {
    if (
      this.program.name &&
      this.program.description &&
      this.program.duration > 0 &&
      this.program.intensity
    ) {
      this.isSubmitted = true;
      console.log('Training Program Created:', this.program);
    } else {
      alert('Please fill in all the fields.');
    }
  }

  resetForm(): void {
    this.program = {
      name: '',
      description: '',
      duration: 0,
      intensity: '',
    };
    this.isSubmitted = false;
  }

  navigateTo(destination: string): void {
    this.router.navigate(['/', destination]);
  }

  logout(): void {
    localStorage.removeItem('admin-token');
    this.router.navigate(['/home']);
  }
}
