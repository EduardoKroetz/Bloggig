import { Component } from '@angular/core';
import { LoginFormComponent } from "./components/login-form/login-form.component";
import { GoogleButtonComponent } from "../../components/google-button/google-button.component";
import { AuthBgigLogoComponent } from "../../components/auth-bgig-logo/auth-bgig-logo.component";
import { HrOuComponent } from "../../components/hr-ou/hr-ou.component";
import { BgigLogoComponent } from "../../components/bgig-logo/bgig-logo.component";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [LoginFormComponent, GoogleButtonComponent, AuthBgigLogoComponent, HrOuComponent, BgigLogoComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

}
