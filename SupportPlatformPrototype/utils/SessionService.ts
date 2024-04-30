import { Injectable } from '@angular/core';
import { User } from '../shared/user.model';
import { Router } from '@angular/router'; // Import Router

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  private readonly SESSION_KEY = 'userData';
  constructor(private router: Router) {}

  get User(): User | null {
    if (typeof sessionStorage !== 'undefined') {
      const storedData = sessionStorage.getItem(this.SESSION_KEY);
      return storedData ? JSON.parse(storedData) : null;
    } else {
      return null;
    }
  }
  

  set User(value: User) {
    if (typeof sessionStorage !== 'undefined') {
      sessionStorage.setItem(this.SESSION_KEY, JSON.stringify(value));
    }
  }

  sessionStart(user: User) {
    this.User = user;
  }

  sessionDestroy() {
    if (typeof sessionStorage !== 'undefined') {
      sessionStorage.removeItem(this.SESSION_KEY);
      this.router.navigate(['']);
    }
  }

  hasSession(): boolean {
    return this.User !== null;
  }
}
