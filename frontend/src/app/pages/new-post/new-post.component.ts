import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PostService } from '../../services/post.service';
import { AlertModalService } from '../../services/alert-modal.service';
import { IPostForm, IPostFormError, PostFormComponent } from "../../components/post-form/post-form.component";

@Component({
  selector: 'app-new-post',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, PostFormComponent],
  templateUrl: './new-post.component.html',
  styleUrls: ['./new-post.component.css']
})
export class NewPostComponent{
  thumbnailBase64 = "";
  postForm : IPostForm = { title: "", content: "", tags: "", thumbnail: null, thumbnailBase64: "", thumbnailLink: null };
  postFormError : IPostFormError = {errorContent: null, errorThumbnail: null, errorTitle: null };
  
  constructor (private postService: PostService, private alertService: AlertModalService) {}

  handleSubmit(){
    const tags = this.postForm.tags.replace('#', '').split(" ");

    this.postService.createPostAsync(this.postForm.title, this.postForm.content, this.postForm.thumbnailBase64, tags).subscribe(
      (res) => {
        this.alertService.toggleModal();
        this.alertService.modalMessage = "Post criado com sucesso!"
        setTimeout(() => {
          window.location.href = "/feed"
        }, 1000)
      },
      (error) => {
        var errorMessage : string = error.error.message;
        if (!errorMessage){
          this.alertService.toggleModal();
          this.alertService.modalMessage = "Ocorreu um erro ao criar o post"
        }

        if (errorMessage.includes("título")) {
          this.postFormError.errorTitle = errorMessage;
        }else if (errorMessage.includes("conteúdo")) {
          this.postFormError.errorContent = errorMessage;
        }else {
          this.alertService.toggleModal();
          this.alertService.modalMessage = errorMessage
        }
      }
    )
  }
}
