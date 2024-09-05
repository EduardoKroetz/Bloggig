import { Component, EventEmitter, Input,  OnChanges,  Output, SimpleChanges } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

export interface IPostForm {
  title: string;
  content: string;
  tags: string;
  thumbnailBase64: string;
  thumbnail: File | null;
  thumbnailLink: string | null;
}

export interface IPostFormError {
  errorTitle: string | null
  errorContent: string | null
  errorThumbnail: string | null
}

@Component({
  selector: 'app-post-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './post-form.component.html',
  styleUrls: ['./post-form.component.css']
})
export class PostFormComponent implements OnChanges {
  @Output() onSubmit = new EventEmitter<IPostForm>();
  @Input() postForm: IPostForm = { title: "", content: "", tags: "", thumbnail: null, thumbnailBase64: "", thumbnailLink: null };
  @Input() postFormError: IPostFormError = {errorContent: null, errorThumbnail: null, errorTitle: null }
  isSumbmitted = false;

  ngOnChanges(changes: SimpleChanges): void {
    this.isSumbmitted = false;
  }

  onThumbnailChange(event: any) {
    if (!this.postForm) return;

    const file = event.target.files[0];
    if (!file) return;

    this.postFormError.errorThumbnail = null;
    this.postForm.thumbnailBase64 = "";
    const maxSizeInMB = 1;

    const fileSizeInMB = file.size / 1024 / 1024;
    if (fileSizeInMB > maxSizeInMB) {
      this.postFormError.errorThumbnail = 'O arquivo é muito grande! Por favor, selecione uma imagem menor que ' + maxSizeInMB + 'MB.';
      return;
    }

    const reader = new FileReader();
    reader.onload = (e: ProgressEvent<FileReader>) => {
      this.postForm!.thumbnailBase64 = (e.target as FileReader).result?.toString().split(',')[1] || '';
    };
    reader.readAsDataURL(file);

    this.postForm.thumbnail = file; 
  }

  handleSubmit(event: SubmitEvent) {
    event.preventDefault();

    this.postFormError.errorContent = null;
    this.postFormError.errorThumbnail = null;
    this.postFormError.errorTitle = null

    if (this.postForm) {
      this.onSubmit.emit(this.postForm); 
    }
  }
}
