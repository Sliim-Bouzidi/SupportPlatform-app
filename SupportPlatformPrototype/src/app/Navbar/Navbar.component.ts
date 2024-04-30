import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-Navbar',
  templateUrl: './Navbar.component.html',
  styleUrls: ['./Navbar.component.css']
})
export class NavbarComponent implements OnInit {
  items: any;
  tenantname: string = "";

  constructor(public router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.items = [
      { label: 'Home', icon: 'pi pi-home', routerLink: ['/home'] },
      { label: 'Profile', icon: 'pi pi-user', routerLink: ['/profile'] },
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
  }
}
