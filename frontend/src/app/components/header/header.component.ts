import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { BgigLogoComponent } from "../bgig-logo/bgig-logo.component";
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { UserProfileImgComponent } from "../user-profile-img/user-profile-img.component";
import { UserProfileService } from '../../services/user-profile.service';
import { RegisterButtonComponent } from "../register-button/register-button.component";
import { LoginButtonComponent } from "../login-button/login-button.component";
import User from '../../interfaces/User';
import { CloseIconComponent } from "../close-icon/close-icon.component";
import { UserIconComponent } from "../user-icon/user-icon.component";
import { GearIconComponent } from "../gear-icon/gear-icon.component";
import { LogoutButtonComponent } from "../logout-button/logout-button.component";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [BgigLogoComponent, RouterLink, CommonModule, UserProfileImgComponent, RegisterButtonComponent, LoginButtonComponent, CloseIconComponent, UserIconComponent, GearIconComponent, LogoutButtonComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit{
  user : User | null = null
  modalIsOpen = false;

  constructor (private userProfileService: UserProfileService) {}

  userProfileUrl = ""

  ngOnInit(): void {
    this.userProfileService.userProfile$.subscribe(data => {
      this.user = data.user;
      if (this.user){
        this.userProfileUrl = `/users/${this.user.id}`
      }
    })
  }

  toggleModal() {
    this.modalIsOpen = !this.modalIsOpen;
  }

  onClickOutModal(event: Event){
    if (event.target === event.currentTarget)
      this.toggleModal();
  }
}

