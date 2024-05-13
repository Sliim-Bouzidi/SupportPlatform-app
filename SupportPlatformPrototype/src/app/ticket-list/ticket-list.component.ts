import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { SessionService } from '../../../utils/SessionService';
import { Ticket } from '../../../shared/ticket.model';
import { TicketService } from '../../../shared/ticket.service';
import { Router, ActivatedRoute } from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser';
import { ConfirmationService, Message } from 'primeng/api';


@Component({
  selector: 'app-ticket-list',
  templateUrl: './ticket-list.component.html',
  styleUrls: ['./ticket-list.component.css']
})
export class TicketListComponent implements OnInit {

  constructor(
    public serviceT: TicketService,
    private sanitizer: DomSanitizer,
    public router: Router,
    private route: ActivatedRoute,
    public serviceM: MessageService,
    public ServiceS: SessionService,
    public ConfirmationService : ConfirmationService,
  ) {}

  tickets: Ticket[] = []
  tenantname: string = "";
  originalList: Ticket[] = [];

  ngOnInit() {
    if (this.ServiceS.hasSession()) {
      const ticketCreated = localStorage.getItem('ticketCreated');
      if (ticketCreated === 'true') {
        setTimeout(() => {
          this.serviceM.add({ severity: 'success', summary: 'Success', detail: 'Ticket Created Successfully' });
          localStorage.removeItem('ticketCreated'); // Remove ticketCreated from local storage
        }, 0);
      }
    }
  
    this.route.params.subscribe(params => {
      this.tenantname = params['tenantname'];
      sessionStorage.setItem('tenantname', this.tenantname);
      this.serviceT.getTicketsByTenant(this.tenantname).subscribe({
        next: (response: Ticket[]) => {
          this.tickets = response;
          this.originalList = response;
          console.log(this.tickets);
        }
      });
    });
  }

  deleteTicket(ticketId: string): void {
    this.tickets = this.tickets.filter(item => item.ticketID !== ticketId);

    this.serviceT.deleteTicket(ticketId).subscribe(
      () => {
       
        console.log(this.tickets);
        // Handle any further actions upon successful deletion
        this.serviceM.add({ severity: 'success', summary: 'Success', detail: 'Ticket Deleted Successfully' });
      },
      error => {
        console.error('Error deleting ticket:', error);
        // Check if the error is due to ticket not found
        if (error.status === 404) {
          console.log('Ticket not found.');
          // Handle the case where the ticket was not found
          this.serviceM.add({ severity: 'warn', summary: 'Warning', detail: 'Ticket not found.' });
        } else if (error.status === 200 || error.status === 204) {
          // If the status code indicates successful deletion, display success message
          console.log('Ticket deleted successfully.');
          this.serviceM.add({ severity: 'success', summary: 'Success', detail: 'Ticket Deleted Successfully' });
        } else {
          // Handle other server errors
          this.serviceM.add({ severity: 'error', summary: 'Error', detail: 'Failed to delete ticket.' });
        }
      }
    );
  }
  
  

  selectedTicket: any
  items: any[] = [
    { label: 'Delete', icon: 'pi pi-trash', command: () => this.confirm1(this.selectedTicket.ticketID) },
    { label: 'View', icon: 'pi pi-eye' },
    { label: 'Export', icon: 'pi pi-file-export' },
  ];
  
  
  getPriorityColor(resultat: string): string {
    switch (resultat) {
      case "Low":
        return "info";
      case "Emergency":
        return "danger";
      case "High":
        return "warning";
      case "Medium":
        return "primary";
      case "Critical":
        return "danger";
      default:
        return "secondary";
    }
  }

  searchkey: string = "";

  Search() {
    if (this.searchkey.trim() !== '') {
      this.tickets = this.originalList.filter(item =>
        Object.values(item).some(val =>
          val !== null && val.toString().toLowerCase().includes(this.searchkey.toLowerCase())
        )
      );
    }
  }

  ResetList() {
    this.tickets = this.originalList;
    this.searchkey = '';
  }

  visible: boolean = false;

  showModel() {
    console.log("works");
    this.visible = !this.visible;
  }

  description = "";

  descriptionModel(description: string) {
    this.description = description;
  }

  navigateToTicketDetails(ticket: any) {
    this.router.navigate(['/TicketDetails', this.tenantname, ticket]);
  }


  confirm1(id: string) {
    // Use the confirmation service to display a confirmation dialog
    this.ConfirmationService.confirm({
      message: 'Are you sure to delete this ticket ?',
      header: 'Ticket Deletion',
      icon: 'pi pi-exclamation-triangle pi-lg',
      accept: () => {
            // Call the onDelete method to perform the deletion
            this.deleteTicket(id);
        },
        acceptLabel: 'Oui',
        rejectLabel: 'Non',
        acceptButtonStyleClass: 'p-button-danger',
        rejectButtonStyleClass: 'p-button-secondary',
        reject: () => {
            // Logic to execute when the user cancels deletion
            console.log('Deletion cancelled');
            // You can put any action you want to perform upon cancellation here
        }

    });
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




