import { TestBed } from '@angular/core/testing';

import { TrainingprogramsService } from './trainingprograms.service';

describe('TrainingprogramsService', () => {
  let service: TrainingprogramsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TrainingprogramsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
