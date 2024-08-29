import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  registerUser(username: string, emaiL: string, password: string){
    return this.http.post<any>(`${this.apiUrl}/auth/register`, { username, emaiL, password }, { withCredentials: true })
  }
}
