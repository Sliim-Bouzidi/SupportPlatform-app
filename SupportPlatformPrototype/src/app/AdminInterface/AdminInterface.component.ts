import { Component, OnInit } from '@angular/core';
import { SessionService } from '../../../utils/SessionService';
import { User } from '../../../shared/user.model';
import { UserService } from '../../../shared/user-service.service';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-AdminInterface',
  templateUrl: './AdminInterface.component.html',
  styleUrls: ['./AdminInterface.component.css']
})
export class AdminInterfaceComponent implements OnInit {
  tenantname: string = "";
  userCount: number = 0; // Property to hold the user count
  constructor(
    public ServiceS: SessionService,
    public ServiceUser: UserService,
    public router: Router, private route: ActivatedRoute
  ) { }



  users: User[] = [];
  originalList:User[] = [];
  


  ngOnInit() {

    
    
    this.getUserCount(); // Fetch user count when component initializes

      // Retrieve tenantname from session storage
      const storedTenantName = sessionStorage.getItem('tenantname');
      if (storedTenantName !== null) {
        this.tenantname = storedTenantName;
      }
    
        this.route.params.subscribe(params => {
          this.tenantname = params['tenantname']; // corrected
        });


    this.ServiceUser.getAllUsers().subscribe({
      next: (response: User[]) => {
        this.users = response;
        this.originalList = response;
        console.log(this.users[0].roles[0].roleValue);
        
      }
    });



    this.getUserRoles()
    
    

    
  }


  getUserCount() {
    this.ServiceUser.getAllUsers().subscribe(users => {
      this.userCount = users.length; // Update user count based on the length of the user array
    });
  }
  
  getToken(): string | null {
    return localStorage.getItem('token'); // Assuming token is stored in local storage
  }

  logout()
  {
    this.ServiceS.sessionDestroy();
    localStorage.removeItem('token');
  }


  userRoles: {name: string} [] = []
  getUserRoles(): void {
    this.ServiceUser.getUserRoles()
      .subscribe(
        roles => {
          // Transform array of strings into array of objects
          this.userRoles = roles.map(role => ({ name: role }));
          console.log('User roles:', this.userRoles);
        },
        error => {
          console.error('Error fetching user roles:', error);
        }
      );
  }
  








  searchkey: string = "";

  Search() {
    if (this.searchkey.trim() !== '') {
      this.users = this.originalList.filter(item =>
        Object.values(item).some(val =>
          val !== null && val.toString().toLowerCase().includes(this.searchkey.toLowerCase())
        )
      );
    }
  }

  ResetList() {
    this.users = this.originalList;
    this.searchkey = '';
  }

  visible: boolean = false;

  showModel() {
    console.log("works");
    this.visible = !this.visible;
  }

  
 /* navigateToTicketDetails(ticket: any) {
    this.router.navigate(['/TicketDetails', this.tenantname, ticket]);
  }*/


}
