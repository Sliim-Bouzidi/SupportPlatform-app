/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { BotIconServiceService } from './BotIconService.service';

describe('Service: BotIconService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BotIconServiceService]
    });
  });

  it('should ...', inject([BotIconServiceService], (service: BotIconServiceService) => {
    expect(service).toBeTruthy();
  }));
});
