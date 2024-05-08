import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environment/environment.development';

@Injectable({
  providedIn: 'root'
})
export class TagsService {

  private apiUrl: string = environment.apiBaseUrl + '/Tag';

  constructor(private http: HttpClient) { }

  getTagNames(ticketId: string): Observable<any> {
    const url = `${this.apiUrl}?ticketId=${ticketId}`;
    return this.http.get(url);
  }


  updateTagsForTicket(ticketId: string, tags: string[]): Observable<any> {
    const url = `${this.apiUrl}/updatetags/${ticketId}`;
    return this.http.put(url, tags);
  }
}
