import { Component, OnInit } from '@angular/core';
import { Ticket } from '../../../shared/ticket.model';
import { TicketService } from '../../../shared/ticket.service';
import { ActivatedRoute, Router } from '@angular/router';
import { response } from 'express';
import { DomSanitizer, } from '@angular/platform-browser';
import { tick } from '@angular/core/testing';




@Component({
  selector: 'app-ticket-list',
  templateUrl: './ticket-list.component.html',
  styleUrls: ['./ticket-list.component.css'] // Changed styleUrl to styleUrls
})
export class TicketListComponent implements OnInit {

  constructor
  (
    public serviceT: TicketService,
    private route: ActivatedRoute,
    private sanitizer: DomSanitizer,
    public router: Router


  ) {} // Added semicolon here


  

tickets : Ticket[] = []
tenantname:string =""
originalList : Ticket[] = []



  ngOnInit() {
    this.route.params.subscribe(params => {
      this.tenantname = params['tenantname']; // corrected
      sessionStorage.setItem('tenantname', this.tenantname); // Store tenantname in session storage
    });



    this.serviceT.getTicketsByTenant(this.tenantname).subscribe({
      next:(response) => {
        this.tickets = response
        this.originalList = response
        console.log(this.tickets)
      }
    })



      /* this.route.params.subscribe(params => {
      const ticketId = params['ticketId']; // Get ticket ID from URL
      if (ticketId) {
        // If ticket ID exists in URL, fetch tenant name and navigate
        this.fetchTenantName(ticketId);
      } else {
        // If no ticket ID, proceed with regular initialization
        this.initialize();
      }
    });
  }

  initialize() {
    this.tenantname = sessionStorage.getItem('tenantname') || '';
    this.serviceT.getTicketsByTenant(this.tenantname).subscribe({
      next: (response) => {
        this.tickets = response;
        this.originalList = response;
      }
    });
  }

  fetchTenantName(ticketId: string) {
    this.serviceT.getTenantNameByTicketId(ticketId).subscribe(
      (tenantName: string) => {
        sessionStorage.setItem('tenantname', tenantName);
        this.router.navigate(['/TicketList', tenantName]); // Replace ticket ID with tenant name
      },
      (error) => {
        console.error('Error fetching tenant name:', error);
        // Handle error (e.g., show error message)
      }
    );*/


  }



  getPriorityColor(resultat: string) {
    if (resultat === "Low") {
      return 'info';
    } else if (resultat === "Emergency") {
      return 'danger';
    } else if (resultat === "High"){
      return 'warning';
    } else if (resultat === "Medium") {
      return 'primary'; 
    } else if (resultat === "Critical")  {
      return 'danger'; // Just return the color code without the hash symbol
    } else {
      return 'secondary';
    }
  }
  searchkey:string = ""

  Search() {
    if (this.searchkey.trim() !== '') {
        // Filter the list based on the searchkey
        this.tickets = this.tickets.filter(item =>
            Object.values(item).some(val =>
                val !== null && val.toString().toLowerCase().includes(this.searchkey.toLowerCase())
            )
        );
    }
  }



  ResetList() {
    // Reset the list to its original state
    this.tickets = this.originalList;
    this.searchkey = '';
  }

  visible: boolean = false
  showModel(){
    console.log("works")
    this.visible = !this.visible
  }

  description = ""
  descriptionModel(description: string){
    this.description = description
  }



  navigateToTicketDetails(ticket: any) {
    // Navigate to the ticket details page using Angular router
    this.router.navigate(['/TicketDetails',this.tenantname,ticket]);
  }
  



 /* getSeverity(resultat: string): string {
    switch (resultat) {
      case 'Conforme':
        return 'success'; // Light green
      case 'danger':
        return '#FFC107'; // Amber
      case 'primary':
        return '#122151'; // Light blue
      default:
        return 'red'; // Default to light blue for unrecognized values
    }
  }*/
}




