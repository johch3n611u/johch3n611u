import { TestBed } from '@angular/core/testing';

import { SelfAccountCheckService } from './self-account-check.service';

describe('SelfAccountCheckService', () => {
  let service: SelfAccountCheckService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SelfAccountCheckService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
