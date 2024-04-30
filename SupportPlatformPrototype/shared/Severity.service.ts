import { Injectable } from '@angular/core';
import { Severity } from './Severity.model';
import { environment } from '../environment/environment.development';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'; // Import Observable from RxJS

@Injectable({
  providedIn: 'root'
})
export class SeverityService {

  private apiUrl: string = environment.apiBaseUrl + '/Severity';

  constructor(private http: HttpClient) { }


  getAllSeverityNames(): Observable<Severity[]> {
    return this.http.get<Severity[]>(this.apiUrl);
  }

}
