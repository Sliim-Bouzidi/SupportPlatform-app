import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environment/environment.development';

@Injectable({
  providedIn: 'root'
})
export class TagsService {

  private apiUrl: string = environment.apiBaseUrl + '/Tags';

  constructor(private http: HttpClient) { }

  // Method to fetch tag names from the API
  getTagNames(): Observable<string[]> {
    return this.http.get<string[]>(this.apiUrl); // Using this.apiUrl instead of environment.apiUrl
  }
}
