import { Injectable } from '@angular/core';
import { Status } from './Status.model';
import { environment } from '../environment/environment.development';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'; // Import Observable from RxJS

@Injectable({
  providedIn: 'root'
})
export class StatusService {

  private apiUrl: string = environment.apiBaseUrl + '/Status';

  constructor(private http: HttpClient) { }



getAllStatusNames(): Observable<Status[]> {
  return this.http.get<Status[]>(this.apiUrl);
}

}
