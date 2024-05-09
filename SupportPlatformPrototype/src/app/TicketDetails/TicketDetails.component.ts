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
  tags: string [] = []
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
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    // Check if running on server to determine whether to use PrimeNG Quill
    this.usePrimeNGQuill = !isPlatformServer(this.platformId);
  }

  userOptions: {name: string} [] = []
  AssignTo: any
  title:string = ""


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
    });


    
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
  const assignToUpdateData = {
    assignTo: this.AssignTo.name
  };

  this.ServiceT.updateTicket(this.ticketID, assignToUpdateData).subscribe({
    next: (response) => {
      console.log('AssignTo update response:', response);
    },
    error: (error) => {
      console.error('Error updating assignTo:', error);
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
    },
    error: (error) => {
      console.error('Error updating ticket without assignTo:', error);
    }
  });
}


  UpdateTags(){
    this.ServiceTags.updateTagsForTicket(this.ticketID, this.tags).subscribe({
      next: (response) =>{
        console.log(response)
      }
    })
  }
  

  showcomment(){
    this.visible1 = !this.visible1
  }

  showAssignTo(){
    this.visible2 = !this.visible2
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
  
  
  
}
