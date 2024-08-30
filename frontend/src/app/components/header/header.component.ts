import { Component } from '@angular/core';
import { BgigLogoComponent } from "../bgig-logo/bgig-logo.component";
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { UserProfileImgComponent } from "../user-profile-img/user-profile-img.component";
import { UserProfileService } from '../../services/user-profile.service';
import { RegisterButtonComponent } from "../register-button/register-button.component";
import { LoginButtonComponent } from "../login-button/login-button.component";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [BgigLogoComponent, RouterLink, CommonModule, UserProfileImgComponent, RegisterButtonComponent, LoginButtonComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  constructor (public userProfileService: UserProfileService) {}

}
