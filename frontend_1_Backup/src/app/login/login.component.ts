import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
  FormsModule,
} from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage: string = '';
  isLoading: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.loginForm.invalid) {
      return;
    }

    this.errorMessage = '';
    this.isLoading = true;

    const { email, password } = this.loginForm.value;

    this.authService.emailExists(email).subscribe({
      next: (exists: boolean) => {
        if (!exists) {
          this.isLoading = false;
          this.errorMessage = 'This email is not registered.';
          return;
        }

        this.authService.login(email, password).subscribe({
          next: (token: string) => {
            this.isLoading = false;
            if (token) {
              this.router.navigate(['/user']);
            } else {
              this.errorMessage = 'Login failed: No token received.';
            }
          },
          error: (err: unknown) => {
            this.isLoading = false;
            console.error('Login error:', err);
            this.errorMessage = 'Login failed: Invalid credentials.';
          },
        });
      },
      error: (err: unknown) => {
        this.isLoading = false;
        console.error('Email check failed:', err);
        this.errorMessage = 'An error occurred while checking email.';
      },
    });
  }
}
