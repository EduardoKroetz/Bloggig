import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UserIconComponent } from "../user-icon/user-icon.component";
import { CloseIconComponent } from "../close-icon/close-icon.component";
import { GearIconComponent } from "../gear-icon/gear-icon.component";
import { LogoutButtonComponent } from "../logout-button/logout-button.component";
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-menu-modal',
  standalone: true,
  imports: [UserIconComponent, CloseIconComponent, GearIconComponent, LogoutButtonComponent,RouterLink, CommonModule],
  templateUrl: './menu-modal.component.html',
  styleUrl: './menu-modal.component.css'
})
export class MenuModalComponent {
  @Input() userProfileUrl = "";
  @Input() modalIsOpen = false;
  @Output() closeModal = new EventEmitter<void>();

  onClickOutModal(event: MouseEvent) {
    if (event.target === event.currentTarget) {
      this.closeModal.emit();
    }
  }

  leaveModal() {
    this.closeModal.emit()
  }
}
