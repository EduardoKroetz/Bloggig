import { Component } from '@angular/core';
import { PostComponent } from "../../components/post/post.component";
import { CommonModule } from '@angular/common';
import Post from '../../interfaces/Post';
import { FeedService } from '../../services/feed.service';

@Component({
  selector: 'app-feed',
  standalone: true,
  imports: [PostComponent, CommonModule],
  templateUrl: './feed.component.html',
  styleUrl: './feed.component.css'
})
export class FeedComponent {
  posts : Post[] = [];
  pageSize = 10;
  pageNumber = 1;
  loadingPosts = true;

  constructor (private feedService: FeedService) {
    this.getPosts();
  }

  getPosts(){
    this.feedService.getPostsAsync(this.pageSize, this.pageNumber).subscribe(
      (res : any) => {
        this.posts = res.data;
        console.log(this.posts);
        this.loadingPosts = false;
      }
    )
  }
}
