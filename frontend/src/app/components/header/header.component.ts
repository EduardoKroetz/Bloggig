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

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [BgigLogoComponent, RouterLink, CommonModule, UserProfileImgComponent, RegisterButtonComponent, LoginButtonComponent, CloseIconComponent, UserIconComponent, GearIconComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit{
  user : User | null = null
  modalIsOpen = true;

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

  toggleModal() {
    this.modalIsOpen = !this.modalIsOpen;
  }

  onClickOutModal(event: Event){
    if (event.target === event.currentTarget)
      this.toggleModal();
  }
}

