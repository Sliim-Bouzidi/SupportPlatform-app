import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformServer } from '@angular/common';
import { Ticket } from '../../../shared/ticket.model';
import { TicketService } from '../../../shared/ticket.service';
import { DomSanitizer, } from '@angular/platform-browser';
import { TicketHistoryService } from '../../../shared/TicketHistory.service';
import { CommentService } from '../../../shared/comment.service';
import { Comment  } from '../../../shared/comment.model';
import { UserService } from '../../../shared/user-service.service';
import { response } from 'express';
import { TagsService } from '../../../shared/Tags.service';
import { TaggableItem } from '../../../shared/taggableitem.model';
import { SessionService } from '../../../utils/SessionService';
import { UserRoles } from '../../../shared/UserRoles.model';
import { MessageService } from 'primeng/api';
import { StatusService } from '../../../shared/Status.service';
import { Status } from '../../../shared/Status.model';
import { StatusHistory } from '../../../shared/StatusHistory.model';
import { StatusHistoryService } from '../../../shared/StatusHistory.service';


@Component({
  selector: 'app-TicketDetails',
  templateUrl: './TicketDetails.component.html',
  styleUrls: ['./TicketDetails.component.css']
})
export class TicketDetailsComponent implements OnInit {
  tenantname: string = '';
  ticketID: string = '';
  visible: boolean = false;
  closable: boolean = false;
  htmlEditor: string = '';
  usePrimeNGQuill: boolean;
  currentContentIndex: number = 0;
  maxIndex: number = 2;
  ticket : any
  isEditing: boolean = false;
  editedTitle: string = '';
  updatedFields: any = {}; // Object to hold updated fields
  tagNames: string[] = []; // Array to hold tag names
  //tagNames: any[] = []; // Array to hold tag names
  ticketHistories: any[] = [];
  comment : string = "";
  comments: any[] = [];
  placeholder = "Add a comment here !"
  visible1: boolean = false
  visible2: boolean = false
  visible3: boolean = false
  tags: string [] = []
  sidebarVisible = false
  //usernames: Map<string, string> = new Map(); // Map to store usernames by user ID
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private ServiceT: TicketService,
    private ServiceH: TicketHistoryService,
    private ServiceC: CommentService,
    private sanitizer: DomSanitizer,
    private ServiceU: UserService,
    private ServiceTags: TagsService,
    public ServiceS: SessionService,
    public serviceM: MessageService,
    public ServiceStatus: StatusService,
    public ServiceStatusHistory: StatusHistoryService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    // Check if running on server to determine whether to use PrimeNG Quill
    this.usePrimeNGQuill = !isPlatformServer(this.platformId);
  }


  
  statuses: any; // Array to hold the list of statuses
  StatusOptions: { name: string }[] = [];
  userOptions: {name: string} [] = []
  AssignTo: any
  title:string = ""
  
  //statusName: any; // Array to hold the list of statuses
 // StatusOptions: { name: string }[] = [];

  ngOnInit() {
    
    
    this.ServiceU.getAllUsers().subscribe({
      next:(response: any) => {
        for (let i = 0; i < response.length; i++) {
          this.userOptions.push({ name: response[i].email });
        }
      },
      error: (error) => {
        console.error('Error fetching users:', error);
      }
    });



    console.log(this.tenantname);
    this.route.params.subscribe(params => {
      this.ticketID = params['ticketID']; // corrected
      this.tenantname = params['tenantname'];
      this.loadComments(); // Load comments when component initializes
      console.log(this.statusHistory)
    });



     this.ServiceStatus.getAllStatusNames().subscribe(
      (response: any) => {
        // Loop over the response array and populate options array
        for (let i = 0; i < response.length; i++) {
          this.StatusOptions.push({ name: response[i] });
        }
      },
      (error) => {
        console.error('Error fetching statuses:', error);
      }
    );

    
    this.loadTicketHistory();




      this.ServiceTags.getTagNames(this.ticketID).subscribe({
        next: (response) =>{
          this.tags = response
          console.log(this.tags)
        }
      })

    this.ServiceT.getTicketById(this.ticketID).subscribe({
      
      next: (ticket: Ticket) => {
        this.ticket = ticket;
        console.log(this.ticket);
       // this.tags = [];
       /* for (let i = 0; i < ticket.tags.length; i++) {
          this.tags.push(ticket.tags[i].tagName);
        }*/

         //tags are a property of the ticket object
         if (this.ticket.tags) {
          //Transform tags object into an array of tag names
          this.tagNames = this.ticket.tags.map((tagObject: any) => tagObject.tag.tagName);
        }
      }
      
    });
    
    

  

   
  }

  showSidebar(){
    this.sidebarVisible = !this.sidebarVisible
  }
  
  statusHistory: StatusHistory[] = []
  loadStatusHistory(){
    
    this.ServiceStatusHistory.getStatusHistory(this.ticketID).subscribe({
      next: (response) => {
        this.statusHistory = response
      }
    })

  }


  loadComments() {
    this.ServiceC.getComments(this.ticketID).subscribe(
      (data: Comment[]) => {
        this.comments = data;
      //  this.fetchUsernamesForComments(); // Fetch usernames for comments
      },
      (error) => {
        console.log(error);
      }
    );
  }


  verifcomment(){
    if (this.comment == ""){
      this.verif = false
    }
    else  {
      this.verif = true
    }
  }



  verif : boolean = false
  AddComment(){
   // this.comments.push(Comment)
   
    if (this.comment == ""){
      this.verif = false
    }
    else{
      this.verif = true
      this.ServiceC.addComments(this.ticketID, this.comment).subscribe({
        next: (response) => {
          this.loadComments()
        }
      })
    }
    
  
  }



  // Method to update only the assignTo property
  updateAssignTo() {
    console.log("works")
  
  
 // Assuming "Mohamed" is the default value when assignTo is not selected
const assignToUpdateData = {
  assignTo: this.AssignTo ? this.AssignTo.name : this.ticket.assignTo,
  statusName: this.statuses ? this.statuses.name : this.ticket.status,
  title: this.ticket.title,
  description: this.ticket.description
};

    
  

    this.ServiceT.updateTicket(this.ticketID, assignToUpdateData).subscribe({
      next: (response) => {
        console.log('AssignTo update response:', response);
        this.serviceM.add({ severity: 'success', summary: 'Success', detail: 'Ticket Updated Successfully' });
      },
      error: (error) => {
        this.serviceM.add({ severity: 'success', summary: 'Success', detail: 'Ticket Updated Successfully' });
        // Handle the error appropriately
      }
    });
  }


  

// Method to update other properties excluding assignTo
updateTicketWithoutAssignTo() {
  // Remove the tags property from the ticket object
  const { tags, ...updateDataWithoutTags } = this.ticket;
  
  this.ServiceT.updateTicket(this.ticketID, updateDataWithoutTags).subscribe({
    next: (response) => {
      console.log('Update without assignTo response:', response);
      this.serviceM.add({ severity: 'success', summary: 'Success', detail: 'Ticket Updated Successfully' });
    },
    error: (error) => {
      console.error('Error updating ticket without assignTo:', error);
      this.serviceM.add({ severity: 'success', summary: 'Success', detail: 'Ticket Updated Successfully' });
    }
  });
}


  UpdateTags(){
    this.ServiceTags.updateTagsForTicket(this.ticketID, this.tags).subscribe({
      next: (response) =>{
        console.log(response)
         this.serviceM.add({ severity: 'success', summary: 'Success', detail: 'Ticket Updated Successfully' });
      }
    })
  }
  



  showcomment(){ 
    this.visible1 = !this.visible1
  }

  showAssignTo(){
    this.visible2 = !this.visible2
  }

  showStatuses(){
    this.visible3 = !this.visible3
  }

 /* fetchUsernamesForComments() {
    // Iterate through comments and fetch usernames for each user ID
    for (const comment of this.comments) {
      this.ServiceU.getUsernameById(comment.userId).subscribe(
        (username: string) => {
          this.usernames.set(comment.userId, username);
        },
        (error) => {
          console.error('Error fetching username:', error);
        }
      );
    }
  }*/

  loadTicketHistory() {
    this.ServiceH.getTicketHistory(this.ticketID).subscribe(
      (data: any[]) => {
        this.ticketHistories = data;
        console.log('Ticket history:', this.ticketHistories);
      },
      (error) => {
        console.error('Error fetching ticket history:', error);
      }
    );
  }




  
   

  navigateToTicketList(tenantname: string) {
    console.log(tenantname);
    this.router.navigate(['/TicketList', tenantname]);
  }

  showTicketInfo() {
    this.visible = !this.visible;
  }

  // Method to show the previous content
  showPreviousContent() {
    if (this.currentContentIndex > 0) {
      this.currentContentIndex--;
    }
  }

  // Method to show the next content
  showNextContent() {
    // Add conditions if you have multiple contents
    if (this.currentContentIndex < this.maxIndex) {
      this.currentContentIndex++;
    }
  }

  showContent(index: number) {
    this.loadStatusHistory()
    this.currentContentIndex = index;

  }


  formatDate(dateString: string): string {
    const date = new Date(dateString);
    return `${date.toLocaleDateString()} ${date.toLocaleTimeString()}`;
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

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  decodeToken(): string {
    const token = this.getToken();
    if (token) {
        const tokenPayload = JSON.parse(atob(token.split('.')[1])); // Decode the token
        return tokenPayload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || 'Unknown'; // Extract role
    } else {
        return 'Unknown';
    }
}

showStatus(role1: string, role2: string, role3: string): boolean {
    const userRole = this.decodeToken();
    return (
        userRole === role1 ||
        userRole === role2 ||
        userRole === role3 
       
    );
}

disabled = true
verif1(){
 return this.disabled = false
}

resetChanges() {
  // Reload the ticket details to reset changes
  this.ServiceT.getTicketById(this.ticketID).subscribe({
    next: (ticket: Ticket) => {
      this.ticket = ticket;
      // Reset any variables holding changes
      this.updatedFields = {};
      this.tagNames = this.ticket.tags.map((tagObject: any) => tagObject.tag.tagName);
    },
    error: (error) => {
      console.error('Error resetting changes:', error);
    }
  });
}

getStatusColor(status: string): string {
  switch (status) {
      case 'In Progress':
          return '#28a745'; // green
      case 'Transferred to Level 3':
          return '#007bff'; // blue
      case 'Resolved':
          return '#ffc107'; // yellow
      case 'On Hold':
          return '#dc3545'; // red
      case 'New':
          return '#6c757d'; // gray
      case 'Transferred to ADO':
          return '#20c997'; // teal
      case 'Open':
          return '#17a2b8'; // cyan
      case 'Closed':
          return '#6610f2'; // purple
      case 'Transferred to Level 2':
          return '#17a2b8'; // cyan
      case 'Pending':
          return '#6c757d'; // gray
      default:
          return 'gray'; // default color
  }
}

  
}
