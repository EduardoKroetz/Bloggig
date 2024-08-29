import { Component } from '@angular/core';
import { RegisterButtonComponent } from "../register-button/register-button.component";
import { LoginButtonComponent } from "../login-button/login-button.component";
import { CommonModule } from '@angular/common';
import { UserProfileService } from '../../services/user-profile.service';
import { UserIconComponent } from "../user-icon/user-icon.component";

@Component({
  selector: 'app-user-profile-img',
  standalone: true,
  imports: [RegisterButtonComponent, LoginButtonComponent, CommonModule, UserIconComponent],
  templateUrl: './user-profile-img.component.html',
  styleUrl: './user-profile-img.component.css'
})
export class UserProfileImgComponent {
  constructor (public userProfileService: UserProfileService) {}
}
