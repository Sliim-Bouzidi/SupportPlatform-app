import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environment/environment.development';
import { Comment } from './comment.model';



@Injectable({
  providedIn: 'root'
})
export class CommentService {

  private apiUrl: string = environment.apiBaseUrl + '/Comment'; // Base URL for ticket operations

constructor(private http: HttpClient) { }


  getComments(ticketID: string): Observable<Comment[]> {
  return this.http.get<Comment[]>(`${this.apiUrl}?TicketID=${ticketID}`);
}


  addComments(ticketID: string, comment: string): Observable<Comment> {
  return this.http.post<Comment>(`${this.apiUrl}/?ticketID=${ticketID}`, { text: comment });
}

}
