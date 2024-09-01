import { Component, OnInit } from '@angular/core';
import { PostComponent } from "../../components/post/post.component";
import { CommonModule } from '@angular/common';
import Post from '../../interfaces/Post';
import { FeedService } from '../../services/feed.service';
import { error } from 'console';
import { AlertModalService } from '../../services/alert-modal.service';

@Component({
  selector: 'app-feed',
  standalone: true,
  imports: [PostComponent, CommonModule],
  templateUrl: './feed.component.html',
  styleUrl: './feed.component.css'
})
export class FeedComponent implements OnInit {
  posts : Post[] = [];
  pageSize = 10;
  pageNumber = 1;
  loadingPosts = true;

  constructor (private feedService: FeedService, private alertModal: AlertModalService) {}

  ngOnInit(): void {
    this.getPosts();
  }

  getPosts(){
    this.feedService.getPostsAsync(this.pageSize, this.pageNumber).subscribe(
      (res : any) => {
        this.posts = res.data;
        this.loadingPosts = false;
      },
      (error) => {
        this.alertModal.toggleModal();
        this.alertModal.modalMessage = "Não foi possível obter os dados do servidor"
      }
    )
  }
}
