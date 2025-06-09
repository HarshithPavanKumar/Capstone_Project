import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import { AuthService } from '../auth.service';
import { CoachRegisterDto } from '../interfaces/coachregisterdto';

interface CoachRegisterDTO {
  Name: string;
  Email: string;
  Password: string;
  Specialty: string;
  ExperienceLevel: string;
  CreatedBy?: string;
  CreatedDateTime: string;
}

@Component({
  selector: 'app-coach-register',
  templateUrl: './coach-register.component.html',
  styleUrls: ['./coach-register.component.css'],
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
})
export class CoachRegisterComponent {
  registerForm: FormGroup;
  submitted = false;
  successMessage = '';
  errorMessage = '';

  experienceLevels = ['Expert', 'Advance', 'Intermediate', 'Beginner'];

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      name: ['', [Validators.required, Validators.pattern(/^[A-Za-z\s]+$/)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      specialty: ['', Validators.required],
      experienceLevel: ['', Validators.required],
    });
  }

  get f() {
    return this.registerForm.controls;
  }

  onSubmit(): void {
    this.submitted = true;
    this.successMessage = '';
    this.errorMessage = '';

    if (this.registerForm.invalid) {
      this.errorMessage = 'Please fill all fields correctly.';
      return;
    }

    const coachData: CoachRegisterDto = {
      ...this.registerForm.value,
      createdBy: 'system',
    };

    this.authService.registerCoach(coachData).subscribe({
      next: () => {
        this.successMessage = 'Registration successful!';
        this.registerForm.reset();
        this.submitted = false;
      },
      error: (err) => {
        console.error('Registration failed:', err);
        this.errorMessage = 'Registration failed. Try again.';
      },
    });
  }
}
