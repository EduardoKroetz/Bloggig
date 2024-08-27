import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EyeClosedIconComponent } from './eye-closed-icon.component';

describe('EyeClosedIconComponent', () => {
  let component: EyeClosedIconComponent;
  let fixture: ComponentFixture<EyeClosedIconComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EyeClosedIconComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EyeClosedIconComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
