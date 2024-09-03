import { Injectable } from '@angular/core';
import Post from '../interfaces/Post';

@Injectable({
  providedIn: 'root'
})
export class PostModalService {
  postModalIsOpen = false;
  post : Post = null!;

  showPostModal(post: Post) {
    this.post = post;
    let body = document.querySelector("body")!;
    body.style.overflow = "hidden";
    this.postModalIsOpen = true;
  }

  closeModal() {
    let body = document.querySelector("body")!;
    body.style.overflow = "auto";
    this.postModalIsOpen = false;
  }
}
