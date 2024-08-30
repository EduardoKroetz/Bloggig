import { Component, Input } from '@angular/core';
import { RegisterButtonComponent } from "../register-button/register-button.component";
import { LoginButtonComponent } from "../login-button/login-button.component";
import { CommonModule } from '@angular/common';
import { UserIconComponent } from "../user-icon/user-icon.component";
import User from '../../interfaces/User';

@Component({
  selector: 'app-user-profile-img',
  standalone: true,
  imports: [RegisterButtonComponent, LoginButtonComponent, CommonModule, UserIconComponent],
  templateUrl: './user-profile-img.component.html',
  styleUrl: './user-profile-img.component.css'
})
export class UserProfileImgComponent {
  @Input() user : User | null | undefined = null;
}
