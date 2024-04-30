import { Injectable } from '@angular/core';
import { Priority } from './Priority.model';
import { environment } from '../environment/environment.development';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'; // Import Observable from RxJS

@Injectable({
  providedIn: 'root'
})
export class PriorityService {



  private apiUrl: string = environment.apiBaseUrl + '/Priorirty';

  constructor(private http: HttpClient) { }


  
  getAllPrioritiesNames(): Observable<Priority[]> {
  return this.http.get<Priority[]>(this.apiUrl);
}

}
