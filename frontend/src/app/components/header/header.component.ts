import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { BgigLogoComponent } from "../bgig-logo/bgig-logo.component";
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { UserProfileImgComponent } from "../user-profile-img/user-profile-img.component";
import { UserProfileService } from '../../services/user-profile.service';
import { RegisterButtonComponent } from "../register-button/register-button.component";
import { LoginButtonComponent } from "../login-button/login-button.component";
import User from '../../interfaces/User';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [BgigLogoComponent, RouterLink, CommonModule, UserProfileImgComponent, RegisterButtonComponent, LoginButtonComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit{
  user : User | null = null

  constructor (private userProfileService: UserProfileService) {}

  userProfileUrl = ""

  ngOnInit(): void {
    this.userProfileService.authenticatedUser$.subscribe(user => {
      this.user = user;
      if (user){
        this.userProfileUrl = `/users/${user.id}`
      }
    })
  }

}
