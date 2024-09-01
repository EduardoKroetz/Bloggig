import { Component, Input } from '@angular/core';
import Post from '../../interfaces/Post';
import { CommonModule } from '@angular/common';
import { PostModalComponent } from "../post-modal/post-modal.component";

@Component({
  selector: 'app-profile-post',
  standalone: true,
  imports: [CommonModule, PostModalComponent],
  templateUrl: './profile-post.component.html',
  styleUrl: './profile-post.component.css'
})
export class ProfilePostComponent {
  @Input() post : Post | null = null;
  postModalIsOpen = false;
  thePostModal : Post = null!;

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
