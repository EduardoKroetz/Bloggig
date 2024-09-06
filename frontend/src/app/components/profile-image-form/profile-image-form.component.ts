import { Component } from '@angular/core';
import { AlertModalService } from '../../services/alert-modal.service';
import { UserProfileService } from '../../services/user-profile.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile-image-form',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './profile-image-form.component.html',
  styleUrl: './profile-image-form.component.css'
})
export class ProfileImageFormComponent {
  imagePreview: string | ArrayBuffer | null = null;
  base64Image: string | null = null;

  constructor (private alertModalService: AlertModalService, private userService: UserProfileService) {}

  onFileSelected(event: Event): void {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files[0]) {
      const file = fileInput.files[0];
      const reader = new FileReader();
      
      reader.onload = (e) => {
        const base64String = reader.result as string;
  
        this.base64Image = base64String.split(',')[1]; 
        this.imagePreview = base64String;
      };
  
      reader.readAsDataURL(file);
    }
  }

  onUpload(): void {
    if (this.base64Image) {
      
      this.userService.uploadProfileImage(this.base64Image).subscribe(
        (res) => {
          this.alertModalService.showModal();
          this.alertModalService.modalMessage = "Imagem de perfil atualizada com sucesso!";
          setTimeout(() => { window.location.href = "/feed" }, 2000)
        },
        (error) => {
          this.alertModalService.showModal();
          this.alertModalService.modalMessage = "Não foi possível atualizar a imagem de perfil";
        }
      );
      
    } else {
      this.alertModalService.showModal();
      this.alertModalService.modalMessage = "Não foi possível atualizar a imagem de perfil";
    }
  }
}
