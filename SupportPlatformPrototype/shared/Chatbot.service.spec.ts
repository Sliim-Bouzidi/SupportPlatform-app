/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ChatbotService } from './Chatbot.service';

describe('Service: Chatbot', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ChatbotService]
    });
  });

  it('should ...', inject([ChatbotService], (service: ChatbotService) => {
    expect(service).toBeTruthy();
  }));
});
