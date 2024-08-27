import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { EyeClosedIconComponent } from "../../../../components/eye-closed-icon/eye-closed-icon.component";
import { EyeIconComponent } from "../../../../components/eye-icon/eye-icon.component";

@Component({
  selector: 'app-login-form',
  standalone: true,
  imports: [CommonModule, EyeClosedIconComponent, EyeIconComponent],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.css'
})
export class LoginFormComponent {
  showPassword = false;
  isSubmitted = false;
  errorEmail : string | null = null;
  errorPassword: string | null = null;

  togglePassword() {
    this.showPassword = !this.showPassword;
  }

  handleSubmit(ev: Event){
    ev.preventDefault();

    this.isSubmitted = true;


  }
}
