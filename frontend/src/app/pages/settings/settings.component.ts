import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserSettingsInfoComponent } from "../../components/user-settings-info/user-settings-info.component";
import { SearchPostComponent } from "../../components/search-post/search-post.component";
import Post from '../../interfaces/Post';
import User from '../../interfaces/User';
import { UserProfileService } from '../../services/user-profile.service';

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [CommonModule, UserSettingsInfoComponent, SearchPostComponent],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.css'
})
export class SettingsComponent implements OnInit {
  section : 'info' | 'posts' = 'info';
  posts : Post[] = [];
  user : User | null = null;
  loadingUser = true;
  
  constructor(public userProfileService: UserProfileService) {}

  ngOnInit(): void {
    this.userProfileService.userProfile$.subscribe(data => {
      this.user = data.user;
      this.loadingUser = data.loading;
    })
  }

}
