import { TestBed } from '@angular/core/testing';

import { SharedSubscriptionService } from './shared-subscription.service';

describe('SharedSubscriptionService', () => {
  let service: SharedSubscriptionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SharedSubscriptionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
