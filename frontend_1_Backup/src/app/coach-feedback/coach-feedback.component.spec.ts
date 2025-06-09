import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoachFeedbackComponent } from './coach-feedback.component';

describe('CoachFeedbackComponent', () => {
  let component: CoachFeedbackComponent;
  let fixture: ComponentFixture<CoachFeedbackComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CoachFeedbackComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CoachFeedbackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
