import { TestBed } from '@angular/core/testing';

import { UserTagPointsService } from './user-tag-points.service';

describe('UserTagPointsService', () => {
  let service: UserTagPointsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserTagPointsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
