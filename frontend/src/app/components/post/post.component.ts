import { Component, Input, OnChanges } from '@angular/core';
import Post from '../../interfaces/Post';
import { UserProfileImgComponent } from "../user-profile-img/user-profile-img.component";
import { CommonModule } from '@angular/common';
import { TimeAgoPipe } from "../../pipes/time-ago.pipe";
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-post',
  standalone: true,
  imports: [UserProfileImgComponent, CommonModule, TimeAgoPipe, RouterLink],
  templateUrl: './post.component.html',
  styleUrl: './post.component.css'
})
export class PostComponent implements OnChanges {
  @Input() post: Post = null!;

  authorProfileUrl = ""
  shortPostContent = ""
  fullPostIsOpen = false;

  constructor() {}

  ngOnChanges() {
    if (this.post)
    {
      this.shortPostContent = this.post?.content.substring(0, 80);
      this.fullPostIsOpen = this.post?.content.length > 80 ? false : true;
      this.authorProfileUrl = `/users/${this.post.author.id}`
    }
  }

  showFullPostContent() {
    this.fullPostIsOpen = true;
  }
}
