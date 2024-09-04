import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import Post from '../../interfaces/Post';
import User from '../../interfaces/User';
import { PostService } from '../../services/post.service';
import { AlertModalService } from '../../services/alert-modal.service';
import { UserProfileService } from '../../services/user-profile.service';
import { UserProfileImgComponent } from "../../components/user-profile-img/user-profile-img.component";
import { TimeAgoPipe } from "../../pipes/time-ago.pipe";
import { PostResultComponent } from "./components/post-result/post-result.component";
import { UserResultComponent } from "./components/user-result/user-result.component";

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [CommonModule, UserProfileImgComponent, RouterLink, TimeAgoPipe, PostResultComponent, UserResultComponent],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent {
  reference: string | null = '';
  section : 'posts' | 'users' = 'posts'
  posts : Post[] = []; //Posts que vão ser renderizados por página
  totalPosts : Post[] = [] //Posts totais
  postsGetParams = { pageSize: 10, pageNumber: 1, firstLoading: true, limitReached : false  }
  users: User[] = [] //Usuários que vão ser renderizados por página
  totalUsers : User[] = [];
  usersGetParams = { pageSize: 10, pageNumber: 1, firstLoading: true, limitReached : false}
  loading = true;

  constructor(private route: ActivatedRoute, private postService: PostService, private userService: UserProfileService , private alertModal: AlertModalService) {}

  ngOnInit(): void {
    // Pega a referência da query
    this.route.queryParams.subscribe(params => {
      this.reference = params['reference'] || '';
      this.totalPosts = [];
      this.totalUsers = []
      this.postsGetParams = { pageSize: 10, pageNumber: 1, firstLoading: true, limitReached : false}
      this.usersGetParams = { pageSize: 10, pageNumber: 1, firstLoading: true, limitReached : false}
      if (this.section === 'posts')
        this.handleGetPosts();
      else
        this.handleGetUsers();
    });
  }

  handleGetPosts() {
    this.loading = true;

    if (!this.reference)
      return

    if (this.section === 'users' && this.totalPosts.length > 0)
    {
      this.section = 'posts'
      return
    }

    this.section = 'posts'

    if(this.postsGetParams.limitReached)
    { 
      var nextPosts = this.totalPosts.slice(
        (this.postsGetParams.pageNumber - 1) * this.postsGetParams.pageSize,
        (this.postsGetParams.pageNumber - 1) * this.postsGetParams.pageSize + this.postsGetParams.pageSize); 
      this.postsGetParams.pageNumber = nextPosts.length > 0 ? this.postsGetParams.pageNumber + 1 : this.postsGetParams.pageNumber;
      this.handleSlice();
      return
    }

    const currentPage = this.posts.length > 0 ? this.postsGetParams.pageNumber + 1 : this.postsGetParams.pageNumber;

    this.postService.searchPostsAsync(this.reference, this.postsGetParams.pageSize, currentPage).subscribe(
      (res: any) => {
        if (res.data.length === 0)
          this.postsGetParams.limitReached = true;
        else 
        {
          this.totalPosts = [...this.totalPosts, ...res.data];
        }
        this.postsGetParams.pageNumber = currentPage;
        this.loading = false;
        this.postsGetParams.firstLoading = false;
        this.handleSlice();
      },
      (error) => {
        this.alertModal.showModal();
        this.alertModal.modalMessage = "Ocorreu um erro ao obter os posts"
      }
    )
  }

  handleGetUsers() {
    this.loading = true;
  
    if (!this.reference)
      return;
  
    if (this.section === 'posts' && this.totalUsers.length > 0)
      {
        this.section = 'users'
        return
      }

    this.section = 'users';
  
    if(this.usersGetParams.limitReached) {
      const nextUsers = this.totalUsers.slice(
        (this.usersGetParams.pageNumber - 1) * this.usersGetParams.pageSize,
        (this.usersGetParams.pageNumber - 1) * this.usersGetParams.pageSize + this.usersGetParams.pageSize); 
      this.usersGetParams.pageNumber = nextUsers.length > 0 ? this.usersGetParams.pageNumber + 1 : this.usersGetParams.pageNumber;
      this.handleSlice();
      return;
    }
  
    const currentPage = this.users.length > 0 ? this.usersGetParams.pageNumber + 1 : this.usersGetParams.pageNumber;
  
    this.userService.searchUsersAsync(this.reference, this.usersGetParams.pageSize, currentPage).subscribe(
      (res: any) => {
        if (res.data.length === 0)
          this.usersGetParams.limitReached = true;
        else {
          this.totalUsers = [...this.totalUsers, ...res.data];
          this.usersGetParams.pageNumber = currentPage;
        }
        this.loading = false;
        this.usersGetParams.firstLoading = false;
        this.handleSlice();
      },
      (error) => {
        this.alertModal.showModal();
        this.alertModal.modalMessage = "Ocorreu um erro ao obter os usuários";
      }
    );
  }
  

  handleBackPage() {
    if (this.section === 'posts') {
      if (this.postsGetParams.pageNumber > 1) {
        this.postsGetParams.pageNumber--;
        this.handleSlice();
      }
    } else {
      if (this.usersGetParams.pageNumber > 1) {
        this.usersGetParams.pageNumber--;
        this.handleSlice();
      }
    }
  }

  handleSlice(){
    if (this.section === 'posts') {
      this.posts = this.totalPosts.slice(
        (this.postsGetParams.pageNumber - 1) * this.postsGetParams.pageSize, //De onde começa a pegar os posts
        (this.postsGetParams.pageNumber - 1) * this.postsGetParams.pageSize + this.postsGetParams.pageSize); //De onde termina incrementando 10 posts
      }
    else
      this.users = this.totalUsers.slice(
        (this.usersGetParams.pageNumber - 1) * this.usersGetParams.pageSize, //De onde começa a pegar os usuários
        (this.usersGetParams.pageNumber - 1) * this.usersGetParams.pageSize + this.usersGetParams.pageSize); //De onde termina incrementando 10 usuários
  }
}
