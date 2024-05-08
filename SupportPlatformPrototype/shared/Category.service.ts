import { Injectable } from '@angular/core';
import { Category } from './Category.model';
import { environment } from '../environment/environment.development';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'; // Import Observable from RxJS

@Injectable({
  providedIn: 'root'
})
export class CategoryService {


  private apiUrl: string = environment.apiBaseUrl + '/Category';

constructor(private http: HttpClient) { }


  getAllCategoryNames(): Observable<string[]> {
    return this.http.get<string[]>(this.apiUrl);
  }


}
