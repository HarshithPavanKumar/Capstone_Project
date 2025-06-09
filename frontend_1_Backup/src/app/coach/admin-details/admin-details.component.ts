import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { UserserviceService } from '../../Service/userservice.service';
import { Coach } from '../../interfaces/coach';

@Component({
  selector: 'app-admin-details',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './admin-details.component.html',
  styleUrls: ['./admin-details.component.css'],
})
export class AdminDetailsComponent implements OnInit {
  coachId?: number;
  coachName: string = 'Admin';
  coach?: Coach;
  errorMessage: string = '';
  isLoading = false;

  tokenPayload: any = null;

  constructor(
    private coachService: UserserviceService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    // Parse token to get coach ID if not passed via route
    const token = localStorage.getItem('admin-token');
    if (token) {
      this.tokenPayload = this.parseJwt(token);
      const adminId = this.tokenPayload?.UserId;
      if (adminId && !this.route.snapshot.paramMap.get('id')) {
        this.coachId = +adminId;
        this.searchCoachById();
      }
    }

    // Use ID from route if available
    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      if (id) {
        this.coachId = +id;
        this.searchCoachById();
      } else if (!this.coachId) {
        this.errorMessage = 'No coach ID provided in the route.';
      }
    });
  }

  private parseJwt(token: string): any {
    try {
      const base64Url = token.split('.')[1];
      const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      const jsonPayload = decodeURIComponent(
        atob(base64)
          .split('')
          .map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
          .join('')
      );
      return JSON.parse(jsonPayload);
    } catch (e) {
      console.error('Invalid token', e);
      return null;
    }
  }

  searchCoachById(): void {
    this.errorMessage = '';
    this.coach = undefined;

    if (!this.coachId) {
      this.errorMessage = 'Please enter a valid Coach ID.';
      return;
    }

    this.isLoading = true;
    this.coachService.getCoachById(this.coachId).subscribe({
      next: (data) => {
        this.coach = data;
        this.coachName = data.name;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = 'Coach not found or an error occurred.';
        console.error(err);
        this.isLoading = false;
      },
    });
  }

  navigateTo(destination: string): void {
    this.router.navigate(['/', destination]);
  }

  logout(): void {
    localStorage.removeItem('admin-token');
    this.router.navigate(['/home']);
  }
}
