import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-UserProfile',
  templateUrl: './UserProfile.component.html',
  styleUrls: ['./UserProfile.component.css']
})
export class UserProfileComponent implements OnInit {
  username: string = 'Unknown'; // Default value

  constructor() { }

  ngOnInit() {
    this.username = this.getUsernameFromToken(); // Set username when component initializes
  }

  getUsernameFromToken(): string {
    const token = localStorage.getItem('token'); // Assuming your JWT token key is 'token'
    if (token) {
      const tokenPayload = JSON.parse(atob(token.split('.')[1])); // Decode the token
      return tokenPayload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] || 'Unknown'; // Extract username
    } else {
      return 'Unknown'; // If token is not available or invalid
    }
  }
}
