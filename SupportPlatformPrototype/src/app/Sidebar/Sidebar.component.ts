import { Component, OnInit } from '@angular/core';
import { SessionService } from '../../../utils/SessionService';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-Sidebar',
  templateUrl: './Sidebar.component.html',
  styleUrls: ['./Sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  tenantname = "";
  role: string = "";

  constructor(
    public ServiceS: SessionService,
    public router: Router, 
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.tenantname = params['tenantname'];
    });

    this.decodeToken();
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  decodeToken() {
    const token = this.getToken();
    if (token) {
      const tokenPayload = JSON.parse(atob(token.split('.')[1])); // Decode the token
      this.role = tokenPayload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || 'Unknown'; // Extract role
    } else {
      this.role = 'Unknown';
    }
  }

  logout() {
    this.ServiceS.sessionDestroy();
    localStorage.removeItem('token');
  }
}
