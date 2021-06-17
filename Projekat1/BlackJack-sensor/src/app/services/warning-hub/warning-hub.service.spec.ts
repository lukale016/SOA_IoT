import { TestBed } from '@angular/core/testing';

import { WarningHubService } from './warning-hub.service';

describe('WarningHubService', () => {
  let service: WarningHubService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WarningHubService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
