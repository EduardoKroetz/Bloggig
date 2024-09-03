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
import { PostModalService } from '../../services/post-modal.service';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [CommonModule, UserProfileImgComponent, RouterLink, TimeAgoPipe],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent {
  reference: string | null = '';
  section : 'posts' | 'users' = 'posts'
  posts : Post[] = [];
  postGetParams = { pageSize: 10, pageNumber: 1, firstLoading: true }
  users: User[] = []
  usersGetParams = { pageSize: 10, pageNumber: 1, firstLoading: true}
  loading = true;

  constructor(private route: ActivatedRoute, private postService: PostService, private userService: UserProfileService , private alertModal: AlertModalService, public postModalService: PostModalService) {}

  ngOnInit(): void {
    // Pega a referência da query
    this.route.queryParams.subscribe(params => {
      this.reference = params['reference'] || '';
      this.handleGetPosts()
    });
  }

  handleGetPosts() {
    this.loading = true;
    
    if (!this.reference)
      return

    if (!this.postGetParams.firstLoading && this.section === 'users')
    {
      this.section = 'posts'
      return
    }

    this.section = 'posts'

    this.postService.searchPostsAsync(this.reference, this.postGetParams.pageSize, this.postGetParams.pageNumber).subscribe(
      (res: any) => {
        if (res.data.length > 0) {
          this.usersGetParams.pageNumber++;
        }
        this.postGetParams.pageNumber++;
        this.posts = [...this.posts, ...res.data]
        this.loading = false;
        this.postGetParams.firstLoading = this.postGetParams.firstLoading ? false : true;
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
      return

    if (!this.usersGetParams.firstLoading && this.section === 'posts')
    {
      this.section = 'users'
      return
    }

    this.section = 'users'

    this.userService.searchUsersAsync(this.reference, this.usersGetParams.pageSize, this.usersGetParams.pageNumber).subscribe(
      (res: any) => {
        if (res.data.length > 0) {
          this.usersGetParams.pageNumber++;
        }
        this.users = [...this.users, ...res.data]
        this.loading = false;
        this.usersGetParams.firstLoading = this.usersGetParams.firstLoading ? false : true;
        console.log(res)
      },
      (error) => {
        this.alertModal.showModal();
        this.alertModal.modalMessage = "Ocorreu um erro ao obter os usuários"
      }
    )
  }
}
