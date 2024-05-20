/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { StatusHistoryService } from './StatusHistory.service';

describe('Service: StatusHistory', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [StatusHistoryService]
    });
  });

  it('should ...', inject([StatusHistoryService], (service: StatusHistoryService) => {
    expect(service).toBeTruthy();
  }));
});
