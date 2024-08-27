import { Component } from '@angular/core';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-google-button',
  standalone: true,
  imports: [],
  templateUrl: './google-button.component.html',
  styleUrl: './google-button.component.css'
})
export class GoogleButtonComponent {
  loginWithGoogle = `${environment.apiUrl}/oauth/google/login`;
}
