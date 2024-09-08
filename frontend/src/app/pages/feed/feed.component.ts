import { Component, OnInit } from '@angular/core';
import { PostComponent } from "../../components/post/post.component";
import { CommonModule } from '@angular/common';
import Post from '../../interfaces/Post';
import { AlertModalService } from '../../services/alert-modal.service';
import { PostService } from '../../services/post.service';
import { LoadingComponent } from "../../components/loading/loading.component";
import { Router } from '@angular/router';

@Component({
  selector: 'app-feed',
  standalone: true,
  imports: [PostComponent, CommonModule, LoadingComponent],
  templateUrl: './feed.component.html',
  styleUrl: './feed.component.css'
})
export class FeedComponent implements OnInit {
  posts : Post[] = [];
  pageSize = 15;
  loadingPosts = true;
  error = false;

  constructor (private postService: PostService, private alertModal: AlertModalService, private router: Router) {}

  ngOnInit(): void {
    this.alertModal.modalIsOpen = false;
    this.getPosts();
  }

  getPosts(){
    this.loadingPosts = true;
    this.error = false
    this.postService.getPostsAsync(this.pageSize).subscribe(
      (res : any) => {
        if (res.data.length > 0)
          this.posts = [...this.posts, ...res.data]

        this.loadingPosts = false;
      },
      (error) => {
        this.error = true;
        this.alertModal.showModal();
        if (error.status === 401)
        {
          this.alertModal.modalMessage = "Você não está autenticado, redirecionando..."
          setTimeout(() => { 
            this.router.navigate(["/auth/login"]) 
            this.alertModal.modalIsOpen = false;
          }, 1200)
        }else
        {
          this.alertModal.modalMessage = "Não foi possível obter os dados do servidor"
        }

        this.loadingPosts = false;
      }
    )
  }
}
