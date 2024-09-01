import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserSettingsInfoComponent } from "../../components/user-settings-info/user-settings-info.component";
import { SearchPostComponent } from "../../components/search-post/search-post.component";
import Post from '../../interfaces/Post';
import { PostService } from '../../services/post.service';
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
  section : 'info' | 'posts' = 'posts';
  posts : Post[] = [];
  user : User | null = null;
  
  constructor(public userProfileService: UserProfileService) {}

  ngOnInit(): void {
    this.userProfileService.authenticatedUser$.subscribe(user => {
      this.user = user;
    })
  }

}
