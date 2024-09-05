import { Component, Input, OnChanges, OnInit } from '@angular/core';
import Post from '../../interfaces/Post';
import { UserProfileImgComponent } from "../user-profile-img/user-profile-img.component";
import { CommonModule } from '@angular/common';
import { TimeAgoPipe } from "../../pipes/time-ago.pipe";
import { RouterLink } from '@angular/router';
import { UserProfileService } from '../../services/user-profile.service';
import User from '../../interfaces/User';
import { PostOptionsDropdownComponent } from "../post-options-dropdown/post-options-dropdown.component";
import { FullImageModalComponent } from "../full-image-modal/full-image-modal.component";
import Comment from '../../interfaces/Comment';
import { CommentService } from '../../services/comment.service';
import { AlertModalService } from '../../services/alert-modal.service';
import { CommentComponent } from "../comment/comment.component";
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-post',
  standalone: true,
  imports: [UserProfileImgComponent, CommonModule, TimeAgoPipe, RouterLink, PostOptionsDropdownComponent, FullImageModalComponent, CommentComponent, FormsModule],
  templateUrl: './post.component.html',
  styleUrl: './post.component.css'
})
export class PostComponent implements OnChanges, OnInit {
  @Input() post: Post = null!;
  
  authorProfileUrl = ""
  shortPostContent = ""
  fullPostIsOpen = false;
  isAuthor: boolean = false;
  authUser: User | null = null;
  postOptionsDropDownIsOpen = false;
  postFullImageIsOpen = false;
  comments : { data: Comment[], pageSize: number, pageNumber: number, limitReached: boolean } = { data: [], pageSize: 5, pageNumber: 1, limitReached: false }
  newComment = "";
  commentIsOpen = false;

  constructor(private userService: UserProfileService, private commentService: CommentService, private alertService: AlertModalService) {}

  ngOnInit(): void {
    this.userService.userProfile$.subscribe(data => {
      this.authUser = data.user;
      if (data.user && this.post)
        this.isAuthor = data.user.id === this.post.authorId;
    })
  }

  ngOnChanges() {
    if (this.post)
    {
      if (this.authUser)
        this.isAuthor = this.authUser.id === this.post.authorId;
      this.shortPostContent = this.post?.content.substring(0, 80);
      this.fullPostIsOpen = this.post?.content.length > 80 ? false : true;
      this.authorProfileUrl = `/users/${this.post.author.id}`
    }
  }

  showFullPostContent() {
    this.fullPostIsOpen = true;
  }

  toggleDropDown() {
    this.postOptionsDropDownIsOpen = !this.postOptionsDropDownIsOpen;
  }

  openFullImage() {
    this.postFullImageIsOpen = true;
  }

  closeFullImage() {
    this.postFullImageIsOpen = false;
  }

  handleGetPostComments() {
    if (this.comments.limitReached)
      return

    this.commentService.getPostComments(this.post.id, this.comments.pageSize, this.comments.pageNumber).subscribe(
      (res: any) => {
        if (res.data.length === 0) {
          this.comments.limitReached = true;
        }
        else {
          this.comments.data = [...this.comments.data, ...res.data]
          this.comments.pageNumber++;
        }
      },
      (error) => {
        this.alertService.showModal();
        this.alertService.modalMessage = "Não foi possível obter os comentários do post";
      }
    )
  }

  handleCreateComment() {
    if (!this.authUser)
      return

    this.commentService.createComment(this.post.id, this.newComment).subscribe(
      (res:any) => {
        if (this.comments.pageNumber > 1){
          this.comments.pageNumber--;
        }
        this.comments.data = [];
        this.handleGetPostComments();
        this.post.commentsCount++;
        this.commentIsOpen = false;
        this.newComment = ""
      },
      (error) => {
        this.alertService.showModal();
        this.alertService.modalMessage = "Não foi possível criar o comentário";
      }
    )
  }

  onDeleteComment(comment: Comment){
    this.post.commentsCount--;
    var commentIndex = this.comments.data.indexOf(comment)
    this.comments.data.splice(commentIndex, 1);
    this.comments.pageNumber = 1;
  }

  onUpdateComment(comment: Comment)
  {
    const currentComment = this.comments.data.find(x => x.id === comment.id)!
    const commentIndex = this.comments.data.indexOf(currentComment);
    this.comments.data.splice(commentIndex, 1, comment);
    this.comments.pageNumber = 1;
  }
}
