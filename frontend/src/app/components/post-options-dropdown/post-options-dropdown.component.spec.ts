import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostOptionsDropdownComponent } from './post-options-dropdown.component';

describe('PostOptionsDropdownComponent', () => {
  let component: PostOptionsDropdownComponent;
  let fixture: ComponentFixture<PostOptionsDropdownComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PostOptionsDropdownComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PostOptionsDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
