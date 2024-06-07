/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { BotIconService } from './BotIcon.service';

describe('Service: BotIcon', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BotIconService]
    });
  });

  it('should ...', inject([BotIconService], (service: BotIconService) => {
    expect(service).toBeTruthy();
  }));
});
