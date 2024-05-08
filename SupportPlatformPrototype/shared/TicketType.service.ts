import { Injectable } from '@angular/core';
import { TicketType } from './TicketType.model';
import { environment } from '../environment/environment.development';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'; // Import Observable from RxJS


@Injectable({
  providedIn: 'root'
})

export class TicketTypeService {


  private apiUrl: string = environment.apiBaseUrl + '/TicketType';



constructor(private http: HttpClient) { }


getAllTicketTypeNames(): Observable<TicketType[]> {
  return this.http.get<TicketType[]>(this.apiUrl);
}


}
