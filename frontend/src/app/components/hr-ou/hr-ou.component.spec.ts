import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrOuComponent } from './hr-ou.component';

describe('HrOuComponent', () => {
  let component: HrOuComponent;
  let fixture: ComponentFixture<HrOuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HrOuComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HrOuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
