import { Component } from '@angular/core';
import { BgigLogoComponent } from "../../components/bgig-logo/bgig-logo.component";
import { GoogleButtonComponent } from "../../components/google-button/google-button.component";
import { RegisterFormComponent } from "./components/register-form/register-form.component";
import { AuthBgigLogoComponent } from "../../components/auth-bgig-logo/auth-bgig-logo.component";
import { HrOuComponent } from "../../components/hr-ou/hr-ou.component";

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [BgigLogoComponent, GoogleButtonComponent, RegisterFormComponent, AuthBgigLogoComponent, HrOuComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

}
