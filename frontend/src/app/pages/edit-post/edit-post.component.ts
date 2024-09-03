import { Component, OnInit } from '@angular/core';
import { AlertModalService } from '../../services/alert-modal.service';
import { PostService } from '../../services/post.service';
import { IPostForm, IPostFormError, PostFormComponent } from '../../components/post-form/post-form.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import Post from '../../interfaces/Post';
import { error } from 'console';

@Component({
  selector: 'app-edit-post',
  standalone: true,
  imports: [CommonModule, FormsModule, PostFormComponent],
  templateUrl: './edit-post.component.html',
  styleUrl: './edit-post.component.css'
})
export class EditPostComponent implements OnInit {
  postId: string | null = null;
  post: Post | null = null;
  thumbnailBase64 = "";
  postForm : IPostForm = { title: "", content: "", tags: "", thumbnail: null, thumbnailBase64: "", thumbnailLink: null };
  postFormError : IPostFormError = {errorContent: null, errorThumbnail: null, errorTitle: null };

  constructor (private route: ActivatedRoute,private postService: PostService, private alertService: AlertModalService) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.postId = params.get('id');

      if (this.postId)
      {
        this.postService.getPostsByIdAsync(this.postId).subscribe(
          (res: any) => {
            this.post = res.data;
            if (this.post)
            {
              const formatedTags =  '#' + this.post?.tags.map(t => t.name).join(" #")
              this.postForm = { title: this.post.title, content: this.post.content, tags: formatedTags, thumbnailLink: this.post.thumbnailUrl, thumbnailBase64: "", thumbnail: null  }
            }
          },
          (error) => {
            this.alertService.showModal();
            this.alertService.modalMessage = "Não foi possível obter os dados do post"
          }
        );
      }
    })
  }

  handleSubmit(){
    const tags = this.postForm.tags.replace('#', '').split(" ");
    if (!this.postId)
      return

    this.postService.updatePostAsync(this.postId,this.postForm.title, this.postForm.content, this.postForm.thumbnailBase64, tags).subscribe(
      (res) => {
        this.alertService.toggleModal();
        this.alertService.modalMessage = "Post atualizado com sucesso!"
        setTimeout(() => {
          window.location.href = "/feed"
        }, 1000)
      },
      (error) => {
        var errorMessage : string = error.error?.message;
        if (!errorMessage){
          this.alertService.toggleModal();
          this.alertService.modalMessage = "Ocorreu um erro ao atualizar o post"
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
