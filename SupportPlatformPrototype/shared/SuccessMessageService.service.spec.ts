/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SuccessMessageServiceService } from './SuccessMessageService.service';

describe('Service: SuccessMessageService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SuccessMessageServiceService]
    });
  });

  it('should ...', inject([SuccessMessageServiceService], (service: SuccessMessageServiceService) => {
    expect(service).toBeTruthy();
  }));
});
