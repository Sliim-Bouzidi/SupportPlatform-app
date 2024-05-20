import { Injectable } from '@angular/core';
import { environment } from '../environment/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StatusHistory } from './StatusHistory.model';
@Injectable({
  providedIn: 'root'
})
export class StatusHistoryService {

  private apiUrl: string = environment.apiBaseUrl + '/Status/statusHistory'; // Base URL for ticket operations



constructor(private http: HttpClient) { }


getStatusHistory(ticketId: string): Observable<StatusHistory[]> {
  return this.http.get<StatusHistory[]>(`${this.apiUrl}?TicketID=${ticketId}`);
}
}
