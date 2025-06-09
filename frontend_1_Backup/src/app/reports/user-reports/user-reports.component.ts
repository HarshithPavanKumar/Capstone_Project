import { Component } from '@angular/core';
import { ChartConfiguration } from 'chart.js';
import { UserreportserviceService } from '../../Service/userreportservice.service';
import { NgChartsModule } from 'ng2-charts';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-reports',
  imports: [CommonModule, NgChartsModule],
  templateUrl: './user-reports.component.html',
  styleUrl: './user-reports.component.css',
})
export class UserReportsComponent {
  info: any = {};
  categorySummary: any[] = [];
  barChartData: ChartConfiguration<'bar'>['data'] = {
    labels: [],
    datasets: [
      {
        label: 'Counts',
        data: [],
        backgroundColor: '#0d6efd',
      },
    ],
  };
  barChartOptions: ChartConfiguration<'bar'>['options'] = {
    responsive: true,
    plugins: {
      legend: { display: false },
    },
    scales: {
      x: {},
      y: { beginAtZero: true },
    },
  };
  pieChartData: ChartConfiguration<'pie'>['data'] = {
    labels: [],
    datasets: [
      {
        data: [],
        backgroundColor: [
          '#007bff',
          '#28a745',
          '#ffc107',
          '#dc3545',
          '#6f42c1',
          '#20c997',
          '#fd7e14',
          '#6610f2',
          '#198754',
        ],
      },
    ],
  };
  pieChartOptions: ChartConfiguration<'pie'>['options'] = {
    responsive: true,
    plugins: {
      legend: {
        position: 'bottom',
      },
      tooltip: {
        callbacks: {
          label: (tooltipItem) => {
            const dataset = tooltipItem.chart.data.datasets[0];
            const total = dataset.data.reduce(
              (acc: any, val: any) => acc + val,
              0
            );
            const current = dataset.data[tooltipItem.dataIndex] as number;
            const percentage = ((current / total) * 100).toFixed(1);
            const label = tooltipItem.label || '';
            return `${label}: ${current} (${percentage}%)`;
          },
        },
      },
    },
  };
  constructor(private infoService: UserreportserviceService) {}

  ngOnInit(): void {
    this.infoService.getInfo().subscribe((data) => {
      this.info = data;
      console.log(this.info);
      const labels = [
        'Athletes',
        'PerformanceMetrics',
        'TrainingPrograms',
        'Subscriptions',
        'Feedbacks',
      ];

      const values = [
        this.info.athletes,
        this.info.feedbacks,
        this.info.performanceMetrics,
        this.info.subscriptions,
        this.info.trainingPrograms,
      ];
      console.log(values);
      // Summary cards
      this.categorySummary = labels.map((label, i) => ({
        label,
        count: values[i],
      }));
        console.log(this.categorySummary);
      // Chart updates
      this.barChartData = {
        labels: labels,
        datasets: [
          {
            label: 'Counts',
            data: values,
            backgroundColor: '#0d6efd',
          },
        ],
      };
      

      this.pieChartData = {
        labels: labels,
        datasets: [
          {
            data: values,
            backgroundColor: [
              '#007bff',
              '#28a745',
              '#ffc107',
              '#dc3545',
              '#6f42c1',
              '#20c997',
              '#fd7e14',
              '#6610f2',
              '#198754',
            ],
          },
        ],
      };
      
    });
  }
}
