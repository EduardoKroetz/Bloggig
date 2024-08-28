import { Component } from '@angular/core';
import { ErrorModalService } from '../../../../services/error-modal.service';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EyeIconComponent } from "../../../../components/eye-icon/eye-icon.component";
import { EyeClosedIconComponent } from "../../../../components/eye-closed-icon/eye-closed-icon.component";
import { EmailInputComponent } from "../../../../components/email-input/email-input.component";
import { PasswordInputComponent } from "../../../../components/password-input/password-input.component";
import { UsernameInputComponent } from "../../../../components/username-input/username-input.component";
import { RegisterService } from '../../../../services/register.service';

@Component({
  selector: 'app-register-form',
  standalone: true,
  imports: [CommonModule, FormsModule, EyeIconComponent, EyeClosedIconComponent, EmailInputComponent, PasswordInputComponent, UsernameInputComponent, RouterLink],
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.css'
})
export class RegisterFormComponent {
  username = ""
  email = "";
  password = "";
  showPassword = false;
  isSubmitted = false;
  errorEmail : string | null = null;
  errorPassword: string | null = null;

  constructor (private registerService: RegisterService, private errorModalService: ErrorModalService, private router: Router ) {}

  togglePassword() {
    this.showPassword = !this.showPassword;
  }

  handleSubmit(ev: Event){
    ev.preventDefault();

    this.isSubmitted = true;

    this.registerService.registerUser(this.username ,this.email, this.password).subscribe(
      (res) => {
        this.isSubmitted = false;
        //Redirecionar
        this.router.navigate(['/']);
      },
      (error) => {
        console.log(error);
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
