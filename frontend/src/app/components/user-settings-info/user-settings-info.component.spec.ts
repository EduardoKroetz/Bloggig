import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserSettingsInfoComponent } from './user-settings-info.component';

describe('UserSettingsInfoComponent', () => {
  let component: UserSettingsInfoComponent;
  let fixture: ComponentFixture<UserSettingsInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserSettingsInfoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserSettingsInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
