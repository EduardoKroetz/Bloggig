import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import User from '../interfaces/User';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService {
  apiUrl = environment.apiUrl;
  user : User | null = null;
  loadingProfile = true;
  constructor(private http: HttpClient) {
    this.getUserInfo();
  }

  getUserInfo() {
    this.http.get(`${this.apiUrl}/users`, { withCredentials: true } ).subscribe(
      (res: any) => {
        this.user = res.data;
        this.loadingProfile = false;
      },
      () => {
        this.loadingProfile = false
      }
    )
  }
}
