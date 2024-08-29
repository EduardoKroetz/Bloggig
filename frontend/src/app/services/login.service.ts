import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  postLoginAsync(email: string, password: string) {
    var loginData = { email, password }
    return this.http.post<any>(`${this.apiUrl}/auth/login`, loginData, { withCredentials: true });
  }
}
