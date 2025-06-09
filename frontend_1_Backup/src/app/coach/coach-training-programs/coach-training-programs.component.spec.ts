import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoachTrainingProgramsComponent } from './coach-training-programs.component';

describe('CoachTrainingProgramsComponent', () => {
  let component: CoachTrainingProgramsComponent;
  let fixture: ComponentFixture<CoachTrainingProgramsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CoachTrainingProgramsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CoachTrainingProgramsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
