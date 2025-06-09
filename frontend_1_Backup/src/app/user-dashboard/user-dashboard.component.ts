import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { UserserviceService } from '../Service/userservice.service';
import { Athlete } from '../athlete-details/athlete-details.component';
import { ChatComponent } from '../chat/chat.component';

@Component({
  selector: 'app-user-dashboard',
  standalone: true,
  imports: [RouterModule, ChatComponent],
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css'],
})
export class UserDashboardComponent implements OnInit {
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

  showDropdown = false;

  constructor(
    private router: Router,
    private userService: UserserviceService
  ) {}

  ngOnInit(): void {
    const email = localStorage.getItem('Email');
    if (email) {
      this.userService.getUserByEmail(email).subscribe({
        next: (user) => {
          this.athlete = user;
        },
        error: (err) => {
          console.error(err);
        },
      });
    }
  }

  navigateToCoachDashboard() {
    this.router.navigate(['/coach']);
  }

  navigateToAthleteDetails() {
    this.router.navigate(['/AthleteDetails']);
  }

  navigateTo(destination: string): void {
    this.router.navigate(['/', destination]);
  }

  toggleDropdown(): void {
    this.showDropdown = !this.showDropdown;
  }

  logout(): void {
    localStorage.clear();
    this.router.navigate(['/home']);
  }
}
