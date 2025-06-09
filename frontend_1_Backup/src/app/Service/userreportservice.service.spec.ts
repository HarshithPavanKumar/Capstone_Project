import { TestBed } from '@angular/core/testing';

import { UserreportserviceService } from './userreportservice.service';

describe('UserreportserviceService', () => {
  let service: UserreportserviceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserreportserviceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
