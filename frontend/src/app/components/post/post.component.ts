import { Component, Input, OnChanges, OnInit } from '@angular/core';
import Post from '../../interfaces/Post';
import { UserProfileImgComponent } from "../user-profile-img/user-profile-img.component";
import { CommonModule } from '@angular/common';
import { TimeAgoPipe } from "../../pipes/time-ago.pipe";
import { RouterLink } from '@angular/router';
import { UserProfileService } from '../../services/user-profile.service';
import User from '../../interfaces/User';
import { PostOptionsDropdownComponent } from "../post-options-dropdown/post-options-dropdown.component";

@Component({
  selector: 'app-post',
  standalone: true,
  imports: [UserProfileImgComponent, CommonModule, TimeAgoPipe, RouterLink, PostOptionsDropdownComponent],
  templateUrl: './post.component.html',
  styleUrl: './post.component.css'
})
export class PostComponent implements OnChanges, OnInit {
  @Input() post: Post = null!;
  
  authorProfileUrl = ""
  shortPostContent = ""
  fullPostIsOpen = false;
  isAuthor: boolean = false;
  private authUser: User | null = null;
  postOptionsDropDownIsOpen = false;

  constructor(private userService: UserProfileService) {}

  ngOnInit(): void {
    this.userService.userProfile$.subscribe(data => {
      this.authUser = data.user;
      if (data.user && this.post)
        this.isAuthor = data.user.id === this.post.authorId;
    })
  }

  ngOnChanges() {
    if (this.post)
    {
      if (this.authUser)
        this.isAuthor = this.authUser.id === this.post.authorId;
      this.shortPostContent = this.post?.content.substring(0, 80);
      this.fullPostIsOpen = this.post?.content.length > 80 ? false : true;
      this.authorProfileUrl = `/users/${this.post.author.id}`
    }
  }

  showFullPostContent() {
    this.fullPostIsOpen = true;
  }

  toggleDropDown() {
    this.postOptionsDropDownIsOpen = !this.postOptionsDropDownIsOpen;
  }
}
