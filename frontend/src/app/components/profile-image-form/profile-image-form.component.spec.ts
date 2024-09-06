import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileImageFormComponent } from './profile-image-form.component';

describe('ProfileImageFormComponent', () => {
  let component: ProfileImageFormComponent;
  let fixture: ComponentFixture<ProfileImageFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfileImageFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileImageFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
