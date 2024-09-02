import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PostService } from '../../services/post.service';
import { error } from 'console';
import { AlertModalService } from '../../services/alert-modal.service';

@Component({
  selector: 'app-new-post',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './new-post.component.html',
  styleUrls: ['./new-post.component.css'] // Corrigido para styleUrls
})
export class NewPostComponent implements OnInit {
  tagsArr: string[] = [];
  thumbnailBase64 = "";
  postForm : FormGroup;

  errorTitle: string | null = null;
  errorContent: string | null = null;
  errorThumbnail: string | null = null;

  constructor (private fb: FormBuilder, private postService: PostService, private alertService: AlertModalService) {
    this.postForm = this.fb.group({
      title: [''],
      content: [''],
      tags: [''],
      thumbnail: [null]
    })
  }

  ngOnInit(): void {
    this.postForm.get('thumbnail')?.valueChanges.subscribe((file: File) => {
      this.errorThumbnail = null;
      this.thumbnailBase64 = "";
      const maxSizeInMB = 1;

      if (file) {
        const fileSizeInMB = file.size / 1024 / 1024;
        if (fileSizeInMB > maxSizeInMB) {
          this.errorThumbnail = 'O arquivo é muito grande! Por favor, selecione uma imagem menor que ' + maxSizeInMB + 'MB.';
          return;
        }

        const reader = new FileReader();
        reader.onload = (e: ProgressEvent<FileReader>) => {
          this.thumbnailBase64 = (e.target as FileReader).result?.toString().split(',')[1] || '';
        };
        reader.readAsDataURL(file);
      }
    })
  }

  onFileChange(event: any): void {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.postForm.patchValue({
        thumbnail: file
      });
    }
  }

  handleSubmit(event: SubmitEvent){
    event.preventDefault();

    this.errorContent = null;
    this.errorThumbnail = null;
    this.errorTitle = null

    const title : string = this.postForm.get('title')?.value;
    const content : string = this.postForm.get('content')?.value
    const tagsStr : string = this.postForm.get('tags')?.value
    const tags = tagsStr.replace('#', '').split(" ");

    this.postService.createPostAsync(title, content, this.thumbnailBase64, tags).subscribe(
      (res) => {
        this.alertService.toggleModal();
        this.alertService.modalMessage = "Post criado com sucesso!"
      },
      (error) => {
        var errorMessage : string = error.error.message;
        if (!errorMessage){
          this.alertService.toggleModal();
          this.alertService.modalMessage = "Ocorreu um erro ao criar o post"
        }

        if (errorMessage.includes("título")) {
          this.errorTitle = errorMessage;
        }else if (errorMessage.includes("conteúdo")) {
          this.errorContent = errorMessage;
        }else {
          this.alertService.toggleModal();
          this.alertService.modalMessage = errorMessage
        }
      }
    )
  }
}
