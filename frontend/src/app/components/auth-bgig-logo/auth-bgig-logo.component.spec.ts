import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthBgigLogoComponent } from './auth-bgig-logo.component';

describe('AuthBgigLogoComponent', () => {
  let component: AuthBgigLogoComponent;
  let fixture: ComponentFixture<AuthBgigLogoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AuthBgigLogoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AuthBgigLogoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
