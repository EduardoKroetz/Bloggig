import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { EyeClosedIconComponent } from "../../../../components/eye-closed-icon/eye-closed-icon.component";
import { EyeIconComponent } from "../../../../components/eye-icon/eye-icon.component";
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { EmailInputComponent } from "../../../../components/email-input/email-input.component";
import { PasswordInputComponent } from "../../../../components/password-input/password-input.component";
import { AlertModalService } from '../../../../services/alert-modal.service';
import { AuthService } from '../../../../services/auth.service';

@Component({
  selector: 'app-login-form',
  standalone: true,
  imports: [CommonModule, EyeClosedIconComponent, EyeIconComponent, FormsModule, EmailInputComponent, PasswordInputComponent, RouterLink],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.css'
})
export class LoginFormComponent {
  email = "";
  password = "";
  isSubmitted = false;
  errorEmail : string | null = null;
  errorPassword: string | null = null;

  constructor (private authService: AuthService, private alertModalService: AlertModalService, private router: Router ) {}

  handleSubmit(ev: Event){
    ev.preventDefault();

    this.isSubmitted = true;
    this.authService.login(this.email, this.password).subscribe(
      (res) => {
        this.isSubmitted = false;
        //Redirecionar
        window.location.href = "/"
      },
      (error) => {
        this.isSubmitted = false;
        if (!error.error.message)
        {
          this.alertModalService.toggleModal();
          this.alertModalService.modalMessage = "Ocorreu um erro ao tentar fazer login";
          return
        }
        const errorMessage = error.error.message;
        const lowerErrorMessage = errorMessage.toLowerCase();
        const isErrorEmail = lowerErrorMessage.includes("email");
        const isErrorPassword = lowerErrorMessage.includes("senha");
        if (isErrorEmail && isErrorPassword)
        {
          this.errorEmail = errorMessage;
          this.errorPassword = errorMessage;
        }else if (isErrorEmail){
          this.errorEmail = errorMessage;
        }else if (isErrorPassword){
          this.errorPassword = errorMessage;
        }
        else
        {
          this.alertModalService.toggleModal();
          this.alertModalService.modalMessage = errorMessage;
        }
      }
    )
  }

  resetErrorFields(){
    this.errorEmail = null;
    this.errorPassword = null;
  }
}
