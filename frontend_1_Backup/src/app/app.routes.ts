// app.routes.ts
import { Routes } from '@angular/router';
import { provideRouter } from '@angular/router';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { LoginComponent } from './login/login.component';
import { AthleteDetailsComponent } from './athlete-details/athlete-details.component';
import { PerformanceMetricsComponent } from './performance-metrics/performance-metrics.component';
import { TrainingProgramsComponent } from './training-programs/training-programs.component';
import { CoachFeedbackComponent } from './coach-feedback/coach-feedback.component';

import { AdminLoginComponent } from './admin-login/admin-login.component';
import { HomeComponent } from './home/home.component';
import { CoachTrainingProgramsComponent } from './coach/coach-training-programs/coach-training-programs.component';
import { CreateNewProgramComponent } from './coach/create-new-program/create-new-program.component';
import { AdminDetailsComponent } from './coach/admin-details/admin-details.component';
import { TrainingSessionsComponent } from './training-sessions/training-sessions.component';
import { AthleteRegisterComponent } from './athlete-register/athlete-register.component';
import { UserReportsComponent } from './reports/user-reports/user-reports.component';
import { AdminReportsComponent } from './reports/admin-reports/admin-reports.component';
import { CoachRegisterComponent } from './coach-register/coach-register.component';

export const appRoutes: Routes = [
  { path: 'user', component: UserDashboardComponent },
  { path: 'coach', component: AdminDashboardComponent }, // Corrected here
  { path: 'athletelogin', component: LoginComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'AthleteDetails', component: AthleteDetailsComponent },
  { path: 'PerformanceMetrics', component: PerformanceMetricsComponent },
  { path: 'TrainingProgram', component: TrainingProgramsComponent },
  { path: 'trainingSession', component: TrainingSessionsComponent },
  { path: 'feedback', component: CoachFeedbackComponent },
  { path: 'admin-login', component: AdminLoginComponent },
  { path: 'home', component: HomeComponent },
  { path: 'admin-details/:id', component: AdminDetailsComponent },

  { path: 'admindashboard', component: AdminDashboardComponent },
  { path: 'trainingPrograms/:id', component: CoachTrainingProgramsComponent },
  { path: 'createnewcomponent', component: CreateNewProgramComponent },
  { path: 'athlete-register', component: AthleteRegisterComponent },
  { path: 'admin-register', component: CoachRegisterComponent },
  { path: 'usercharts', component: UserReportsComponent },
  { path: 'admincharts', component: AdminReportsComponent },
];
