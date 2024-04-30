import { Injectable } from '@angular/core';
import { environment } from '../environment/environment.development';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'; // Import Observable from RxJS
import { ProcessFlow } from './ProcessFlow.model';

@Injectable({
  providedIn: 'root'
})
export class ProcessFlowService {


  private apiUrl: string = environment.apiBaseUrl + '/ProcessFlow';

  constructor(private http: HttpClient) { }


  getFirstLevelProcessFlow(): Observable<string[]> {
    return  this.http.get<string[]>(`${this.apiUrl}/FirstLevel`);
  }

  getListOfChildrenOfParent(parentProcessFlowName: string): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/OtherLevels?parentProcessFlowName=${parentProcessFlowName}`);
  }

}
