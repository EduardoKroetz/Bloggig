import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { EyeIconComponent } from "../eye-icon/eye-icon.component";
import { EyeClosedIconComponent } from "../eye-closed-icon/eye-closed-icon.component";

@Component({
  selector: 'app-password-input',
  standalone: true,
  imports: [CommonModule, FormsModule, EyeIconComponent, EyeClosedIconComponent],
  templateUrl: './password-input.component.html',
  styleUrl: './password-input.component.css'
})
export class PasswordInputComponent {
  showPassword = false;
  @Input() password = ""
  @Input() errorPassword: string | null = null;
  @Output() passwordChange = new EventEmitter<string>()

  onPasswordChange(value: string){
    this.password = value;
    this.passwordChange.emit(value);
  }

  togglePassword() {
    this.showPassword = !this.showPassword;
  }
}
