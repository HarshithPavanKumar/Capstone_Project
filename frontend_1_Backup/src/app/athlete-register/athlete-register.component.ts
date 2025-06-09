import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

import { AuthService } from '../auth.service';

@Component({
  selector: 'app-athlete-register',
  templateUrl: './athlete-register.component.html',
  styleUrl: './athlete-register.component.css',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
})
export class AthleteRegisterComponent {
  registerForm: FormGroup;
  submitted = false;
  errorMessage = '';
  successMessage = '';
  genders = ['Male', 'Female'];

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      name: ['', [Validators.required, Validators.pattern(/^[A-Za-z\s]+$/)]],
      age: ['', [Validators.required, Validators.min(10), Validators.max(100)]],
      gender: ['', Validators.required],
      height: [
        '',
        [Validators.required, Validators.min(50), Validators.max(300)],
      ],
      weight: [
        '',
        [Validators.required, Validators.min(20), Validators.max(300)],
      ],
      contact: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  get f() {
    return this.registerForm.controls;
  }

  onSubmit(): void {
    this.submitted = true;
    this.errorMessage = '';
    this.successMessage = '';

    if (this.registerForm.invalid) {
      this.errorMessage = 'Please fill all fields correctly.';
      return;
    }

    const athleteData = {
      ...this.registerForm.value,
      createdBy: 'system',
      createdDateTime: new Date().toISOString(),
    };

    this.authService.registerAthlete(athleteData).subscribe({
      next: () => {
        this.successMessage = 'Registration successful!';
        this.registerForm.reset();
        this.submitted = false;
      },
      error: (err) => {
        console.error('Registration error:', err);
        this.errorMessage = 'Registration failed. Try again.';
      },
    });
  }
}
