import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http'; // Import HttpErrorResponse from @angular/common/http
import { Observable, of, throwError } from 'rxjs'; // Import throwError
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
    return this.http.post<any>(`${this.apiUrl}/CreateTicket`, ticketData).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error instanceof HttpErrorResponse && error.status === 200) {
          // If the status code is 200 but the response cannot be parsed as JSON,
          // return an empty response or handle it in a way suitable for your application.
          return of(null);
        } else {
          // For other HTTP errors, re-throw the error to be handled by the caller.
          return throwError(error);
        }
      })
    );
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
  

  updateTicket(ticketId: string, updateData: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}?TicketID=${ticketId}`, updateData);
  }


  deleteTicket(ticketId: string): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/?TicketID=${ticketId}`).pipe(
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  

}
