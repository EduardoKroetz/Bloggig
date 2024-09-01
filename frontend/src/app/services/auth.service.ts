import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  logOut() {
    return this.http.post(`${this.apiUrl}/auth/logout`, {} , { withCredentials: true })
  }

  login(email: string, password: string) {
    var loginData = { email, password }
    return this.http.post<any>(`${this.apiUrl}/auth/login`, loginData, { withCredentials: true });
  }

  registerUser(username: string, emaiL: string, password: string){
    return this.http.post<any>(`${this.apiUrl}/auth/register`, { username, emaiL, password }, { withCredentials: true })
  }
}
