import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../../shared/user.model';

@Component({
  selector: 'app-Navbar',
  templateUrl: './Navbar.component.html',
  styleUrls: ['./Navbar.component.css']
})
export class NavbarComponent implements OnInit {
  items: any;
  tenantname: string = "";
  userID: string = '';

  constructor(public router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.items = [
      { label: 'Home', icon: 'pi pi-home', command: () => this.navigateToOverview() },
      { label: 'Profile', icon: 'pi pi-user', command: () => this.navigateToUserProfile() },
      { label: 'Settings', icon: 'pi pi-cog', routerLink: ['/settings'] }
      // Add more menu items as needed
    ];
    

    // Retrieve tenantname from session storage
    const storedTenantName = sessionStorage.getItem('tenantname');
    if (storedTenantName !== null) {
      this.tenantname = storedTenantName;
    }

    this.route.params.subscribe(params => {
      this.tenantname = params['tenantname']; // corrected
    });

    this.decodeToken(); // Call decodeToken to extract userID
  }

  decodeToken() {
    const token = this.getToken();
    if (token) {
      console.log('Token:', token);
      const parts = token.split('.');
      console.log('Token parts:', parts);
      const tokenPayload = JSON.parse(atob(parts[1])); // Decode the token
      console.log('Decoded token payload:', tokenPayload);
      this.userID = tokenPayload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] || 'Unknown'; // Extract user ID
    } else {
      this.userID = 'Unknown';
    }
    console.log('User ID:', this.userID);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  navigateToUserProfile() {
    this.router.navigate(['/UserProfile', this.tenantname, this.userID]);
  }

  navigateToOverview() {
    this.router.navigate(['/Overview', this.tenantname]);
  }
}

