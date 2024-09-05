import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnChanges, Output } from '@angular/core';
import Comment from '../../interfaces/Comment';
import { UserProfileImgComponent } from "../user-profile-img/user-profile-img.component";
import { RouterLink } from '@angular/router';
import User from '../../interfaces/User';
import { UserProfileService } from '../../services/user-profile.service';
import { CommentService } from '../../services/comment.service';
import { AlertModalService } from '../../services/alert-modal.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-comment',
  standalone: true,
  imports: [CommonModule, UserProfileImgComponent,RouterLink, FormsModule],
  templateUrl: './comment.component.html',
  styleUrl: './comment.component.css'
})
export class CommentComponent implements OnChanges{
  @Input() comment: Comment | null = null;
  @Output() deleteComment = new EventEmitter<Comment>();
  @Output() updateComment = new EventEmitter<Comment>();

  authUser: User | null = null;
  commentOptionsIsOpen = false;
  isEdit = false;
  newContent = this.comment?.content;

  constructor(private userService: UserProfileService, private commentService: CommentService, private alertService: AlertModalService) {}

  ngOnChanges(): void {
    this.newContent = this.comment?.content;
  }

  ngOnInit(): void {
    this.userService.userProfile$.subscribe(data => {
      this.authUser = data.user;
    })
  }

  handleUpdateComment(event: SubmitEvent) {
    event.preventDefault();

    this.isEdit = false;

    if (!this.comment)
      return

    if (!this.newContent)
      return

    this.commentService.updateComment(this.comment.id, this.newContent).subscribe(
      (res) => {
        this.alertService.showModal();
        this.alertService.modalMessage = "Comentário atualizado com sucesso!"
        this.comment!.content = this.newContent!;
        this.updateComment.emit(this.comment!)
      },
      (error) => {
        this.alertService.showModal();
        this.alertService.modalMessage = "Não foi possível atualizar o comentário"
      } 
    )
  }

  handleDeleteComment() {
    if (!this.comment)
      return

    this.commentService.deleteComment(this.comment.id).subscribe(
      (res) => {
        this.alertService.showModal();
        this.alertService.modalMessage = "Comentário deletado com sucesso!"
        this.deleteComment.emit(this.comment!);
      },
      (error) => {
        this.alertService.showModal();
        this.alertService.modalMessage = "Não foi possível deletar o comentário"
      }
    )
  }

}
