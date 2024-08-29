import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { EyeClosedIconComponent } from "../../../../components/eye-closed-icon/eye-closed-icon.component";
import { EyeIconComponent } from "../../../../components/eye-icon/eye-icon.component";
import { LoginService } from '../../../../services/login.service';
import { FormsModule } from '@angular/forms';
import { ErrorModalService } from '../../../../services/error-modal.service';
import { Router, RouterLink } from '@angular/router';
import { EmailInputComponent } from "../../../../components/email-input/email-input.component";
import { PasswordInputComponent } from "../../../../components/password-input/password-input.component";

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

  constructor (private loginService: LoginService, private errorModalService: ErrorModalService, private router: Router ) {}

  handleSubmit(ev: Event){
    ev.preventDefault();

    this.isSubmitted = true;
    console.log(this.email, this.password)
    this.loginService.postLoginAsync(this.email, this.password).subscribe(
      (res) => {
        this.isSubmitted = false;
        //Redirecionar
        window.location.href = "/"
      },
      (error) => {
        console.log(error.error)
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
          this.errorModalService.toggleModal();
          this.errorModalService.modalMessage = errorMessage;
        }

        this.isSubmitted = false;
      }
    )
  }

  resetErrorFields(){
    this.errorEmail = null;
    this.errorPassword = null;
  }
}
