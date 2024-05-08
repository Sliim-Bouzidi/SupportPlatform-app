import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { SessionService } from '../../../utils/SessionService';
import { Ticket } from '../../../shared/ticket.model';
import { TicketService } from '../../../shared/ticket.service';
import { Router, ActivatedRoute } from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser';

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
    public ServiceS: SessionService
  ) {}

  tickets: Ticket[] = [];
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




