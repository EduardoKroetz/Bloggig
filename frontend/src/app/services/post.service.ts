import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private http: HttpClient) { }

  getPostsAsync(pageSize: number, pageNumber: number)
  {
    return this.http.get(`${environment.apiUrl}/posts?pageSize=${pageSize}&pageNumber=${pageNumber}`)
  }

  getUserPosts(userId: string, pageSize: number, pageNumber: number){
    return this.http.get(`${environment.apiUrl}/posts/users/${userId}?pageSize=${pageSize}&pageNumber=${pageNumber}`);
  }

  createPostAsync(title: string, content: string, base64Thumbnail: string, tags: string[]){
    return this.http.post(`${environment.apiUrl}/posts`,{ title, content, base64Thumbnail, tags }, { withCredentials: true });
  }
}
