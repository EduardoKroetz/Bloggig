import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserTagPointsService {

  constructor(private httpClient: HttpClient) { }

  createUserTagPoint(postId: string){
    return this.httpClient.post(`${environment.apiUrl}/usertagpoints`, { postId  },  { withCredentials: true })
  }
}
