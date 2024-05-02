import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Ticket } from './ticket.model';
import { environment } from '../environment/environment.development';

@Injectable({
  providedIn: 'root'
})
export class TicketService {
  private apiUrl: string = environment.apiBaseUrl + '/ticket'; // Base URL for ticket operations

  constructor(private http: HttpClient) { }

  createTicket(ticketData: Ticket): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/CreateTicket`, ticketData); // Use base URL + specific endpoint
  }

  getTicketsByTenant(tenantName: string): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(`${this.apiUrl}/?TenantName=${tenantName}`);
  }



  getTicketById(ticketId: string): Observable<Ticket> {
    return this.http.get<Ticket>(`${this.apiUrl}/TicketDetails?ticketID=${ticketId}`);
  }



 /* updateTicket(ticketId: string, updatedFields: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${ticketId}`, updatedFields);
  }*/
  

}
