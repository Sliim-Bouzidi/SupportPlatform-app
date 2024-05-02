import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformServer } from '@angular/common';
import { Ticket } from '../../../shared/ticket.model';
import { TicketService } from '../../../shared/ticket.service';
import { response } from 'express';
import { DomSanitizer, } from '@angular/platform-browser';
@Component({
  selector: 'app-TicketDetails',
  templateUrl: './TicketDetails.component.html',
  styleUrls: ['./TicketDetails.component.css']
})
export class TicketDetailsComponent implements OnInit {
  tenantname: string = '';
  ticketID: string = '';
  visible : boolean = false
  closable : boolean = false
  htmlEditor: string = '';
  usePrimeNGQuill: boolean;
  currentContentIndex: number = 0;
  maxIndex : number = 2
  
  ticket :any
  isEditing: boolean = false;
  editedTitle: string = '';
  updatedFields: any = {}; // Object to hold updated fields
  
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private ServiceT: TicketService,
    private sanitizer: DomSanitizer,

    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    // Check if running on server to determine whether to use PrimeNG Quill
    this.usePrimeNGQuill = !isPlatformServer(this.platformId);
  }
  
  
  ngOnInit() {
  console.log(this.tenantname)
    this.route.params.subscribe(params => {
      this.ticketID = params['ticketID']; // corrected
      this.tenantname = params['tenantname'];
    });       
    this.loadTicketDetails(); 

    this.ServiceT.getTicketById(this.ticketID).subscribe({
      next:(response) => {
        this.ticket= response
        console.log(this.ticket)
      }
    })




  }
  loadTicketDetails(): void {
    this.ServiceT.getTicketById(this.ticketID).subscribe((ticket: Ticket) => {
      this.ticket.description = ticket.description; // Assuming 'description' is a field in your Ticket model
    });
  }

  navigateToTicketList(tenantname: string) {
    console.log(tenantname)
    this.router.navigate(['/TicketList', tenantname]);
  }

  /*copyTicketID() {
    const ticketIDElement = document.createElement('textarea');
    ticketIDElement.value = this.ticketID;
    document.body.appendChild(ticketIDElement);
    ticketIDElement.select();
    document.execCommand('copy');
    document.body.removeChild(ticketIDElement);
  }*/


  showTicketInfo(){
    this.visible = !this.visible
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
}
