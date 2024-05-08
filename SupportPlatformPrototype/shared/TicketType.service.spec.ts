/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { TicketTypeService } from './TicketType.service';

describe('Service: TicketType', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TicketTypeService]
    });
  });

  it('should ...', inject([TicketTypeService], (service: TicketTypeService) => {
    expect(service).toBeTruthy();
  }));
});
