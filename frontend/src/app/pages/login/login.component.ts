import { Component } from '@angular/core';
import { LoginFormComponent } from "./components/login-form/login-form.component";
import { BgigLogoComponent } from "../../components/bgig-logo/bgig-logo.component";
import { GoogleButtonComponent } from "../../components/google-button/google-button.component";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [LoginFormComponent, BgigLogoComponent, GoogleButtonComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

}
