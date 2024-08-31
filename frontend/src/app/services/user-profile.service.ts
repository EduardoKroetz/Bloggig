import { Injectable, OnInit } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import User from '../interfaces/User';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService  {
  apiUrl = environment.apiUrl;
  private _authenticatedUser = new BehaviorSubject<User | null>(null);
  authenticatedUser$ = this._authenticatedUser.asObservable();
  loadingProfile = true;
  constructor(private http: HttpClient) {
    this.getAuthenticatedUser();

  }

  getUserById(userId: string) {
    return this.http.get(`${this.apiUrl}/users/${userId}`);
  }

  updateUser(username: string, email: string) {
    return this.http.put(`${this.apiUrl}/users`, { username, email }, { withCredentials: true })
  }

  private getAuthenticatedUser() {
    this.http.get(`${this.apiUrl}/users`, { withCredentials: true } ).subscribe(
      (res: any) => {
        this._authenticatedUser.next(res.data);
        this.loadingProfile = false;
      },
      () => {
        this.loadingProfile = false
      }
    )
  }


}
