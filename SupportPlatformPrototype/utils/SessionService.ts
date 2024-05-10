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
      localStorage.removeItem('token'); // Remove token from local storage
      this.router.navigate(['']);
    }
  }
  

  hasSession(): boolean {
    return this.User !== null;
  }

  hasAdminRole(): boolean {
    const token = this.getToken();
    if (token) {
      const tokenPayload = JSON.parse(atob(token.split('.')[1]));
      const roles = tokenPayload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      return roles && roles.includes('Admin');
    }
    return false;
  }

  private getToken(): string | null {
    return localStorage.getItem('token');
  }
}
