import { Component, Input } from '@angular/core';
import Post from '../../interfaces/Post';
import { CommonModule } from '@angular/common';
import { PostModalComponent } from "../post-modal/post-modal.component";
import { PostModalService } from '../../services/post-modal.service';

@Component({
  selector: 'app-profile-post',
  standalone: true,
  imports: [CommonModule, PostModalComponent],
  templateUrl: './profile-post.component.html',
  styleUrl: './profile-post.component.css'
})
export class ProfilePostComponent {
  @Input() post : Post | null = null;

  constructor (public postModalService: PostModalService) {}
}
