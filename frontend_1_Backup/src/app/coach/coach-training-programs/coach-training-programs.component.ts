import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Personalizedtraining } from '../../interfaces/personalizedtraining';
import { PersonalizedtrainingprogramService } from '../../Service/personalizedtrainingprogram.service';

@Component({
  selector: 'app-coach-training-programs',
  standalone: true,
  imports: [RouterModule, FormsModule, CommonModule],
  templateUrl: './coach-training-programs.component.html',
  styleUrls: ['./coach-training-programs.component.css'],
})
export class CoachTrainingProgramsComponent implements OnInit {
  tokenPayload: any = null;
  coachName: string = 'Admin';
  showProgramDetails = false;
  isLoading = true;
  error = '';
  trainingPrograms: Personalizedtraining | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private trainingService: PersonalizedtrainingprogramService
  ) {}

  ngOnInit(): void {
    this.decodeToken();

    const idParam = this.route.snapshot.paramMap.get('id');
    const id = idParam ? Number(idParam) : null;

    if (id) {
      this.trainingService.getProgramById(id).subscribe({
        next: (data) => {
          this.trainingPrograms = data;
          this.isLoading = false;
        },
        error: (err) => {
          this.error = 'Failed to fetch program.';
          console.error(err);
          this.isLoading = false;
        },
      });
    } else {
      this.error = 'Invalid program ID.';
      this.isLoading = false;
    }
  }

  private decodeToken(): void {
    const token = localStorage.getItem('admin-token');
    if (!token) {
      console.warn('No token found in localStorage.');
      return;
    }

    try {
      const base64Url = token.split('.')[1];
      const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      const jsonPayload = decodeURIComponent(
        atob(base64)
          .split('')
          .map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
          .join('')
      );
      this.tokenPayload = JSON.parse(jsonPayload);
    } catch (error) {
      console.error('Failed to decode JWT:', error);
    }
  }

  toggleDetails(): void {
    this.showProgramDetails = !this.showProgramDetails;
  }

  navigateTo(destination: string): void {
    this.router.navigate([`/${destination}`]);
  }
  logout(): void {
    localStorage.removeItem('admin-token');
    this.router.navigate(['/home']);
  }
}
