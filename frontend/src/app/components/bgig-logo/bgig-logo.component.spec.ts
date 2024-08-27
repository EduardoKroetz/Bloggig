import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BgigLogoComponent } from './bgig-logo.component';

describe('BgigLogoComponent', () => {
  let component: BgigLogoComponent;
  let fixture: ComponentFixture<BgigLogoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BgigLogoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BgigLogoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
