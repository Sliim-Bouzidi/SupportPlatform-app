import { Injectable } from '@angular/core';
import { environment } from '../environment/environment.development';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'; // Import Observable from RxJS
import { tenant } from './tenant.model';
@Injectable({
  providedIn: 'root'
})
export class TenantService {

  private apiUrl: string = environment.apiBaseUrl + '/Tenant';

  constructor(private http: HttpClient) { }


  getTenants(): Observable<tenant[]> {
    return this.http.get<tenant[]>(`${this.apiUrl}`);
  }

}
