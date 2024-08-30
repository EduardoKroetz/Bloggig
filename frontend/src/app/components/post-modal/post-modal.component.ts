import { Component, Input } from '@angular/core';
import { CloseIconComponent } from "../close-icon/close-icon.component";
import { CommonModule } from '@angular/common';
import Post from '../../interfaces/Post';
import { FormsModule } from '@angular/forms';
import { PostComponent } from "../post/post.component";

@Component({
  selector: 'app-post-modal',
  standalone: true,
  imports: [CloseIconComponent, CommonModule, FormsModule, PostComponent],
  templateUrl: './post-modal.component.html',
  styleUrl: './post-modal.component.css'
})
export class PostModalComponent {
  @Input() post : Post = null!;
  @Input() modalIsOpen = false;
  @Input() toggleModal : () => void = () => {}

  closeModalOnClickOutside(event: MouseEvent, modalBackGround: HTMLElement) {
    if (event.target === modalBackGround) {
      this.toggleModal();
    }
  }

}
