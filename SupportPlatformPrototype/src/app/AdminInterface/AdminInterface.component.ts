import { Component, OnInit } from '@angular/core';
import { SessionService } from '../../../utils/SessionService';
import { User } from '../../../shared/user.model';
import { UserService } from '../../../shared/user-service.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api'
import { Role } from '../../../shared/Role.model';
import { tenant } from '../../../shared/tenant.model';
import { UserRoles } from '../../../shared/UserRoles.model';
import { TicketService } from '../../../shared/ticket.service';
import { response } from 'express';

@Component({
  selector: 'app-AdminInterface',
  templateUrl: './AdminInterface.component.html',
  styleUrls: ['./AdminInterface.component.css']
})
export class AdminInterfaceComponent implements OnInit {
  tenantname: string = "";
  userCount: number = 0; // Property to hold the user count
  Allroles: string[] = []; // Initialize roles property
  tickets: any[] = [];
  selectedRoles: string[] = [];
  selectedUser: User | null = null;
  constructor(
    public ServiceS: SessionService,
    public ServiceUser: UserService,
    public router: Router, private route: ActivatedRoute,
    public serviceM: MessageService,
    public ticketService: TicketService
  ) {

    

   }

  

  users: User[] = [];
  originalList:User[] = [];
  data:any
  data2: any;
  options: any;

  selectedProduct: any
  items: any[] = [
    { label: 'Delete', icon: 'pi pi-trash'   },
    { label: 'View', icon: 'pi pi-eye'  },
    { label: 'Export', icon: 'pi pi-file-export' },
  ];
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
                  console.log(this.users)
                  
                }
              });
              


              this.getUserRoles()
              
              
              this.fetchRoles();
              



              this.ticketService.getTicketsByTenant(this.tenantname).subscribe({
                next: (response) => {
                  console.log(response);
                  this.tickets = response;
          
                  
                  






                  // Calculate ticket statuses for each day of the current week
                  const weekData = this.calculateWeeklyStatus();
          
                  this.data2 = {
                    labels: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'],
                    datasets: [
                      {
                        label: 'New',
                        backgroundColor: '#FF6384',
                        data: weekData.new
                      },
                      {
                        label: 'Transferred to ADO',
                        backgroundColor: '#36A2EB',
                        data: weekData.transferredToADO
                      },
                      {
                        label: 'Closed',
                        backgroundColor: '#FFCE56',
                        data: weekData.closed
                      },
                      {
                        label: 'Pending',
                        backgroundColor: '#4BC0C0',
                        data: weekData.pending
                      },
                      {
                        label: 'Transferred to Level 2',
                        backgroundColor: '#9966FF',
                        data: weekData.transferredToLevel2
                      },
                      {
                        label: 'Transferred to Level 3',
                        backgroundColor: '#FF9F40',
                        data: weekData.transferredToLevel3
                      }
                    ]
                  };
          
                  this.options = {
                    scales: {
                      x: {
                        stacked: true,
                        barPercentage: 0.2, // Adjust bar thickness
                        categoryPercentage: 0.5 // Adjust bar thickness
                      },
                      y: {
                        stacked: true
                      }
                    }
                  };
                },
                error: (error) => {
                  console.error('Error fetching tickets:', error);
                }
              });
            
              
    
                 
  }



    calculateWeeklyStatus() {
    const weekStart = this.getWeekStart(new Date());
    const weekEnd = new Date(weekStart);
    weekEnd.setDate(weekStart.getDate() + 6);

    const weekData = {
      new: Array(7).fill(0),
      transferredToADO: Array(7).fill(0),
      closed: Array(7).fill(0),
      pending: Array(7).fill(0),
      transferredToLevel2: Array(7).fill(0),
      transferredToLevel3: Array(7).fill(0)
    };

    this.tickets.forEach(ticket => {
      const ticketDate = new Date(ticket.createdDate);
      if (ticketDate >= weekStart && ticketDate <= weekEnd) {
        const dayIndex = (ticketDate.getDay() + 6) % 7; // Adjust to make Monday = 0
        switch (ticket.status) {
          case 'New':
            weekData.new[dayIndex]++;
            break;
          case 'Transferred to ADO':
            weekData.transferredToADO[dayIndex]++;
            break;
          case 'Closed':
            weekData.closed[dayIndex]++;
            break;
          case 'Pending':
            weekData.pending[dayIndex]++;
            break;
          case 'Transferred to Level 2':
            weekData.transferredToLevel2[dayIndex]++;
            break;
          case 'Transferred to Level 3':
            weekData.transferredToLevel3[dayIndex]++;
            break;
        }
      }
    });

    return weekData;
  }

  getWeekStart(date: Date): Date {
    const day = date.getDay();
    const diff = date.getDate() - day + (day === 0 ? -6 : 1); // Adjust when day is Sunday
    return new Date(date.setDate(diff));
  }



 
  

  
  getUserId(userId: string): void {
    console.log('Clicked UserID:', userId);
    // Perform actions with the userID, such as updating the selected user
    // You can use this method to set the selected user based on the clicked row
  }

  selectUser(user: User): void {
    this.selectedUser = user;
  }





  getUserCount() {
    this.ServiceUser.getAllUsers().subscribe(users => {
      this.users = users;

      // Calculate user counts for each role after users are fetched
      const appUserCount = this.CalculateUsers('AppUser');
      const appSupportL1Count = this.CalculateUsers('AppSupportL1');
      const appSupportL2Count = this.CalculateUsers('AppSupportL2');
      const appSupportL3Count = this.CalculateUsers('AppSupportL3');
      const adminCount = this.CalculateUsers('Admin');

      this.data = {
        labels: ['AppUser', 'AppSupportL1', 'AppSupportL2', 'AppSupportL3', 'Admin'],
        datasets: [
          {
            data: [appUserCount, appSupportL1Count, appSupportL2Count, appSupportL3Count, adminCount],
            backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0', '#9966FF'] // Example background colors for each category
          }
        ]
      };
    },
    
      );
  }

  CalculateUsers(role: string): number {
    return this.users.filter(item => item.roles.some(r => r.roleValue === role)).length;
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
  




  fetchRoles(): void {
    this.ServiceUser.getAllRoles()
      .subscribe(
        Allroles => {
          this.Allroles = Allroles;
          console.log('Roles:', Allroles); // Log roles here
        },
        error => {
          console.error('Error fetching roles:', error);
        }
      );
}

UpdatedRoles: any[] = []
UpdateRole(UserID: string) {
 

  
   
    this.ServiceUser.updateUserRoles(UserID, this.UpdatedRoles).subscribe({
      next:(response) => {
        console.log(response)
        console.log(this.users)
        this.users = response
        this.getUserCount()
      }
    });
  
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
