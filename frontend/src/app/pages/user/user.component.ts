import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import User from '../../interfaces/User';
import { UserProfileService } from '../../services/user-profile.service';
import { CommonModule } from '@angular/common';
import { UserIconComponent } from "../../components/user-icon/user-icon.component";
import { UserProfileImgComponent } from "../../components/user-profile-img/user-profile-img.component";
import Post from '../../interfaces/Post';
import { PostService } from '../../services/post.service';
import { PostModalComponent } from "../../components/post-modal/post-modal.component";
import { ProfilePostComponent } from "../../components/profile-post/profile-post.component";
import { AlertModalService } from '../../services/alert-modal.service';
import { error } from 'console';
import { LoadingComponent } from "../../components/loading/loading.component";
import { SearchPostComponent } from "../../components/search-post/search-post.component";

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [CommonModule, UserIconComponent, UserProfileImgComponent, PostModalComponent, ProfilePostComponent, LoadingComponent, SearchPostComponent],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent implements OnInit {
  user : User | null = null; 
  id : string | null = "";
  loadingUser = true;

  constructor(private route: ActivatedRoute, private userProfileService: UserProfileService, public postService: PostService, private alertModal: AlertModalService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.id = params.get('id');
      if (this.id)
        {
          this.userProfileService.getUserById(this.id).subscribe(
            (res: any) => {
              this.user = res.data;
              this.loadingUser = false;
            },
            (error) => {
              this.loadingUser = false;
              this.alertModal.showModal();
              this.alertModal.modalMessage = "Não foi possível obter os dados do usuário"
            }
          )
        }
    })
  }


}
