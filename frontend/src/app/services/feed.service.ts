import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FeedService {

  constructor(private httpClient: HttpClient) { }

  getPostsAsync(pageSize: number, pageNumber: number)
  {
    return this.httpClient.get(`${environment.apiUrl}/posts?pageSize=${pageSize}&pageNumber=${pageNumber}`)
  }
}
