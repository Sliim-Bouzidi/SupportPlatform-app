// success-message.service.ts
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SuccessMessageService {
  private messageSubject: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);

  constructor() {}

  setMessage(message: string): void {
    this.messageSubject.next(message);
  }

  getMessage(): Observable<string | null> {
    return this.messageSubject.asObservable();
  }
}
