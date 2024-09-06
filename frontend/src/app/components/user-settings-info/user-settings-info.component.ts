import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import User from '../../interfaces/User';
import { UserProfileService } from '../../services/user-profile.service';
import { UserProfileImgComponent } from "../user-profile-img/user-profile-img.component";
import { CommonModule } from '@angular/common';
import { UserIconComponent } from "../user-icon/user-icon.component";
import { FormsModule } from '@angular/forms';
import { AlertModalService } from '../../services/alert-modal.service';
import { ProfileImageFormComponent } from "../profile-image-form/profile-image-form.component";

@Component({
  selector: 'app-user-settings-info',
  standalone: true,
  imports: [UserProfileImgComponent, CommonModule, UserIconComponent, FormsModule, ProfileImageFormComponent],
  templateUrl: './user-settings-info.component.html',
  styleUrl: './user-settings-info.component.css'
})
export class UserSettingsInfoComponent implements OnChanges{
  @Input() user : User | null = null;
  isEditing = false;
  username: string = ""
  email: string = ""

  constructor (private userProfileService: UserProfileService, private alertModalService: AlertModalService) {}

  ngOnChanges(): void {
    if (!this.user)
      return
    this.username = this.user.username || "";
    this.email = this.user.email || "";
  }

  toggleEdit() {
    this.isEditing = !this.isEditing
  }

  cancelEdit(){
    if (!this.user)
      return
    this.username = this.user.username;
    this.email = this.user.email;
    this.isEditing = false;
  }

  handleUpdateUserInfo() {
    this.userProfileService.updateUser(this.username, this.email).subscribe(
      (res: any) => {
        this.alertModalService.modalMessage =  "Usuário atualizado com sucesso!"
      },
      (error) => {
        this.alertModalService.modalMessage =  error.error.message
      }
    )
    this.isEditing = false;
    this.alertModalService.toggleModal();
  }
}
