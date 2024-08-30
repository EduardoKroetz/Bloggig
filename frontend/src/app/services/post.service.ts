import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private http: HttpClient) { }

  getUserPosts(userId: string, pageSize: number, pageNumber: number){
    return this.http.get(`${environment.apiUrl}/posts/users/${userId}?pageSize=${pageSize}&pageNumber=${pageNumber}`);
  }
}
