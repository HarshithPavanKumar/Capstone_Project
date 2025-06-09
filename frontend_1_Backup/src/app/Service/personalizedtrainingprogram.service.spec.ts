import { TestBed } from '@angular/core/testing';

import { PersonalizedtrainingprogramService } from './personalizedtrainingprogram.service';

describe('PersonalizedtrainingprogramService', () => {
  let service: PersonalizedtrainingprogramService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PersonalizedtrainingprogramService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
