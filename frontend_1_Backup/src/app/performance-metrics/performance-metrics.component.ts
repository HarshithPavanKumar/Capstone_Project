import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { UserserviceService } from '../Service/userservice.service';
import { Athlete } from '../athlete-details/athlete-details.component';
import {
  PerformanceMetricService,
  Performance,
} from '../Service/performance-metric.service';

@Component({
  selector: 'app-performance-metrics',
  standalone: true,
  imports: [RouterModule, CommonModule, FormsModule],
  templateUrl: './performance-metrics.component.html',
  styleUrls: ['./performance-metrics.component.css'],
})
export class PerformanceMetricsComponent implements OnInit {
  athlete: Athlete = {
    id: 0,
    name: '',
    email: '',
    password: '',
    age: 0,
    gender: '',
    height: 0,
    weight: 0,
    contact: '',
  };

  performanceMetrics: Performance[] = [];
  editIndex: number | null = null;
  successMessage: string | null = null;

  constructor(
    private router: Router,
    private userServices: UserserviceService,
    private performanceService: PerformanceMetricService
  ) {}

  ngOnInit(): void {
    const email = localStorage.getItem('Email');
    if (email) {
      this.userServices.getUserByEmail(email).subscribe({
        next: (user) => {
          this.athlete = user;
          if (this.athlete) {
            this.performanceService
              .getMetricsByAthleteId(Number(this.athlete.id))
              .subscribe({
                next: (metrics) => {
                  this.performanceMetrics = metrics;
                },
                error: (err) => {
                  console.error('Error fetching metrics:', err);
                },
              });
          }
        },
        error: (err) => {
          console.error('Failed to fetch user:', err);
        },
      });
    }
  }

  toggleEdit(index: number): void {
    this.editIndex = this.editIndex === index ? null : index;
  }

  saveMetric(metric: Performance, index: number): void {
    this.performanceService.updateMetric(metric).subscribe({
      next: () => {
        this.editIndex = null;
        this.successMessage = 'Performance metric updated successfully!';
      },
      error: (err) => {
        console.error('Failed to update metric:', err);
      },
    });
  }

  navigateTo(destination: string): void {
    this.router.navigate(['/', destination]);
  }
  goBack(): void {
    window.history.back(); // Or wherever you want to navigate back to
  }
  logout(): void {
    localStorage.clear();
    this.router.navigate(['/home']);
  }
}
