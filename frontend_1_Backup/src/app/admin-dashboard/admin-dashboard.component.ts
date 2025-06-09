// src/app/admin-dashboard/admin-dashboard.component.ts
import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { UserserviceService } from '../Service/userservice.service';
import { Coach } from '../interfaces/coach';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css'],
  imports:[RouterModule]
})
export class AdminDashboardComponent implements OnInit {
  tokenPayload: any = null;
  coachName: string = 'Admin';

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

  navigateTo(destination: string): void {
    this.router.navigate(['/', destination]);
  }

  logout(): void {
    localStorage.removeItem('admin-token');
    this.router.navigate(['/home']);
  }
}
