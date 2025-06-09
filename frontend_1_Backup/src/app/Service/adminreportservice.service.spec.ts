import { TestBed } from '@angular/core/testing';

import { AdminreportserviceService } from './adminreportservice.service';

describe('AdminreportserviceService', () => {
  let service: AdminreportserviceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AdminreportserviceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
