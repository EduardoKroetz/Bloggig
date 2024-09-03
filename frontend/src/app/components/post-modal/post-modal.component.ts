import { Component } from '@angular/core';
import { CloseIconComponent } from "../close-icon/close-icon.component";
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PostComponent } from "../post/post.component";
import { PostModalService } from '../../services/post-modal.service';

@Component({
  selector: 'app-post-modal',
  standalone: true,
  imports: [CloseIconComponent, CommonModule, FormsModule, PostComponent],
  templateUrl: './post-modal.component.html',
  styleUrl: './post-modal.component.css'
})
export class PostModalComponent {
  constructor (public postModalService: PostModalService) {}

  closeModalOnClickOutside(event: MouseEvent, modalBackGround: HTMLElement) {
    if (event.target === modalBackGround) {
      this.postModalService.closeModal();
    }
  }

}
