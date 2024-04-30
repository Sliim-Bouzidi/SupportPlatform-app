import { Injectable } from '@angular/core';
import { User } from './user.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../environment/environment.development';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  apiUrl: string = environment.apiBaseUrl + '/UserControllers/GetAllUsers'; // Update controller name
  apiUrl2: string = environment.apiBaseUrl + '/UserControllers/Login';

  constructor
  (
    private http: HttpClient,
    public router: Router,

  ) { }

   storeToken(tokenValue: string){
    localStorage.setItem('token', tokenValue);
  }

  getToken() {
    const token = localStorage.getItem('token');
    console.log('Token:', token);
    return token;
  }
  
 /* isLoggedUb(): boolean {
    return !!localStorage.getItem('token');
  }*/

    // Method to perform logout
    logout(): void {
      localStorage.removeItem('token'); 
      this.router.navigate(['']);
      console.log('User logged out successfully');
    }
  


 


  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl);
  }

  login(user: User): Observable<any> {
    // Send credentials to the backend and expect token in response
    return this.http.post(this.apiUrl2, user, { responseType: 'text' });
  }

  

 
  // Method to create HTTP headers
 /* getHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.getToken()}` // Attach token to the headers
    });
  }*/
}
