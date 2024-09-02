import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { PostService } from '../../services/post.service';
import { AlertModalService } from '../../services/alert-modal.service';
import { ConfirmModalService } from '../../services/confirm-modal.service';
import { ConfirmModalComponent } from "../confirm-modal/confirm-modal.component";

@Component({
  selector: 'app-post-options-dropdown',
  standalone: true,
  imports: [CommonModule, ConfirmModalComponent],
  templateUrl: './post-options-dropdown.component.html',
  styleUrl: './post-options-dropdown.component.css'
})
export class PostOptionsDropdownComponent {
  @Input() dropdownIsOpen = false;
  @Input() postId = ""; 
  @Output() closedropdown = new EventEmitter<any>();

  constructor ( private postService: PostService, private alertService: AlertModalService, private confirmModalService: ConfirmModalService ) {}

  onCloseDropdown() {
    this.dropdownIsOpen = false;
    this.closedropdown.emit();
  }

  onDeletePost() {
    this.confirmModalService.toggleModal();
    this.confirmModalService.modalMessage = "Tem certeza que deseja deletar o post? Essa ação é irreversível"
  }

  onConfirmDeletePost() {
    this.postService.deletePostAsync(this.postId).subscribe(
      (res) => {
        window.location.reload();
      },
      (error) => {
        this.alertService.toggleModal();
        this.alertService.modalMessage = "Ocorreu um erro ao deletar o post. Erro: " + error?.error?.message;
      }
    )
  }
}
