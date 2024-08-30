import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import User from '../../interfaces/User';
import { UserProfileService } from '../../services/user-profile.service';
import { CommonModule } from '@angular/common';
import { UserIconComponent } from "../../components/user-icon/user-icon.component";
import { UserProfileImgComponent } from "../../components/user-profile-img/user-profile-img.component";
import Post from '../../interfaces/Post';
import { PostService } from '../../services/post.service';
import { error } from 'console';
import { PostModalComponent } from "../../components/post-modal/post-modal.component";

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [CommonModule, UserIconComponent, UserProfileImgComponent, PostModalComponent],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent implements OnInit {
  user : User | null = null; 
  id : string | null = "";
  posts: Post[] = []
  postsPageSize = 20;
  postsPageNumber = 1;
  loadingUser = true;
  postModalIsOpen = false;
  thePostModal : Post = null!;
  
  constructor(private route: ActivatedRoute, private userProfileService: UserProfileService, private postService: PostService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.id = params.get('id');
      if (this.id)
        {
          this.userProfileService.getUserById(this.id).subscribe(
            (res: any) => {
              this.user = res.data;
              this.loadingUser = false;
            },
            (error) => {
              this.loadingUser = false;
            }
          )
    
          this.postService.getUserPosts(this.id, this.postsPageSize, this.postsPageNumber).subscribe(
            (res: any) => {
              this.posts = res.data;
              console.log(this.posts)
            }
          )
        }
    })
  }

  activePostModal(post: Post)
  {
    this.thePostModal = post;
    this.togglePostModal();
  }

  togglePostModal() {
    this.postModalIsOpen = !this.postModalIsOpen
    let body = document.querySelector("body")!;
    if (this.postModalIsOpen)
      body.style.overflow = "hidden";
    else
      body.style.overflow = "auto";
  }

}
