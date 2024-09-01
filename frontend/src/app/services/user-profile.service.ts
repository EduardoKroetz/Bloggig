import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import User from '../interfaces/User';
import { BehaviorSubject, catchError, map, of } from 'rxjs';
import { AlertModalService } from './alert-modal.service';
import { error, profile } from 'console';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService {
  apiUrl = environment.apiUrl;
  private _userProfile = new BehaviorSubject<{ user: User | null, loading: boolean }>({ user: null, loading: true });
  userProfile$ = this._userProfile.asObservable();

  constructor(private http: HttpClient, private alertService: AlertModalService) {
    this.loadUserProfile()
  }


  getUserById(userId: string) {
    return this.http.get(`${this.apiUrl}/users/${userId}`);
  }

  updateUser(username: string, email: string) {
    return this.http.put(`${this.apiUrl}/users`, { username, email }, { withCredentials: true })
  }
  
  private loadUserProfile() {
    this.http.get<{ data: User }>(`${this.apiUrl}/users`, { withCredentials: true })
      .pipe(
        map(response => ({ user: response.data, loading: false })),
        catchError(error => {
          this.alertService.toggleModal();
          this.alertService.modalMessage = "Não foi possível carregar as informações do usuário"
          return of({ user: null, loading: false })
        })
      )
      .subscribe(profile => this._userProfile.next(profile))
  };
}
