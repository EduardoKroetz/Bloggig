import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserProfileImgComponent } from './user-profile-img.component';

describe('UserProfileImgComponent', () => {
  let component: UserProfileImgComponent;
  let fixture: ComponentFixture<UserProfileImgComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserProfileImgComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserProfileImgComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
