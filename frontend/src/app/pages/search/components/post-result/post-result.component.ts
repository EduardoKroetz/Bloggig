import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { UserProfileImgComponent } from "../../../../components/user-profile-img/user-profile-img.component";
import { RouterLink } from '@angular/router';
import { TimeAgoPipe } from "../../../../pipes/time-ago.pipe";
import { PostModalService } from '../../../../services/post-modal.service';
import Post from '../../../../interfaces/Post';

@Component({
  selector: 'app-post-result',
  standalone: true,
  imports: [CommonModule, UserProfileImgComponent, RouterLink, TimeAgoPipe],
  templateUrl: './post-result.component.html',
  styleUrl: './post-result.component.css'
})
export class PostResultComponent {
  @Input() post : Post = null!;

  constructor (public postModalService: PostModalService ) {}
}
