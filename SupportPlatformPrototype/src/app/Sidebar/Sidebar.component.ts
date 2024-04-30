import { Component, OnInit } from '@angular/core';
import { SessionService } from '../../../utils/SessionService';

@Component({
  selector: 'app-Sidebar',
  templateUrl: './Sidebar.component.html',
  styleUrls: ['./Sidebar.component.css']
})
export class SidebarComponent implements OnInit {

  constructor(public ServiceS: SessionService) { }

  ngOnInit() {
  }

  getToken(): string | null {
    return localStorage.getItem('token'); // Assuming token is stored in local storage
  }


  logout()
  {
    this.ServiceS.sessionDestroy();
    localStorage.removeItem('token');
  }

}
