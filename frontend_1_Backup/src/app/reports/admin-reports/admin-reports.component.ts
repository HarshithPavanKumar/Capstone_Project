import { Component } from '@angular/core';
import { ChartConfiguration } from 'chart.js';
import { NgChartsModule } from 'ng2-charts';
import { AdminreportserviceService } from '../../Service/adminreportservice.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-userreports',
  standalone: true,
  imports: [NgChartsModule, CommonModule],
  templateUrl: './admin-reports.component.html',
  styleUrl: './admin-reports.component.css',
})
export class AdminReportsComponent {
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
  constructor(private infoService: AdminreportserviceService) {}

  ngOnInit(): void {
    this.infoService.getInfo().subscribe((data) => {
      this.info = data;
      console.log(data);
      const labels = [
        'Coaches',
        'AIPredictions',
        'PersonalizedTrainingPrograms',
      ];

      const values = [
        this.info.coaches,
        this.info.aiPredictions,
        this.info.personalizedTrainingPrograms,
      ];

      // Summary cards
      this.categorySummary = labels.map((label, i) => ({
        label,
        count: values[i],
      }));

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
