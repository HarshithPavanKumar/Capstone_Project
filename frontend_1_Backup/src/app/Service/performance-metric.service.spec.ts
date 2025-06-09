import { TestBed } from '@angular/core/testing';

import { PerformanceMetricService } from './performance-metric.service';

describe('PerformanceMetricService', () => {
  let service: PerformanceMetricService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PerformanceMetricService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
