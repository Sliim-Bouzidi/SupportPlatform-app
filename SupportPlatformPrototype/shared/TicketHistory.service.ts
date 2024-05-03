import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environment/environment.development';

@Injectable({
  providedIn: 'root'
})
export class TicketHistoryService {

  private apiUrl: string = environment.apiBaseUrl + '/ticket'; // Use apiUrl instead of baseUrl

  constructor(private http: HttpClient) { }

  getTicketHistory(ticketId: string): Observable<any[]> {
    const url = `${this.apiUrl}/TicketNotes?ticketID=${ticketId}`; // Use apiUrl here
    return this.http.get<any[]>(url);
  }
}
