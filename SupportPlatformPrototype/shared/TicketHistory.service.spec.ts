/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { TicketHistoryService } from './TicketHistory.service';

describe('Service: TicketHistory', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TicketHistoryService]
    });
  });

  it('should ...', inject([TicketHistoryService], (service: TicketHistoryService) => {
    expect(service).toBeTruthy();
  }));
});
