import { Component, Input, OnChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserIconComponent } from "../user-icon/user-icon.component";
import User from '../../interfaces/User';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-user-profile-img',
  standalone: true,
  imports: [CommonModule, UserIconComponent, RouterLink],
  templateUrl: './user-profile-img.component.html',
  styleUrl: './user-profile-img.component.css'
})
export class UserProfileImgComponent implements OnChanges {
  @Input() user : User | null | undefined = null;

  userProfileUrl = ""

  ngOnChanges(): void {
    if (this.user){
      this.userProfileUrl = `users/${this.user.id}`
    }
  }
}
