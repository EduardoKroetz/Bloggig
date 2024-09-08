import { Component, EventEmitter, Output } from '@angular/core';
import { ConfirmModalService } from '../../services/confirm-modal.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-confirm-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './confirm-modal.component.html',
  styleUrl: './confirm-modal.component.css'
})
export class ConfirmModalComponent {
  @Output() confirm = new EventEmitter<any>() 
  isConfirmed = false;

  constructor (public confirmModal: ConfirmModalService) {}

  closeModalOnClickOutside(event: MouseEvent) {
    if (event.target === event.currentTarget) {
      this.confirmModal.toggleModal();
    }
  }

  onConfirm() {
    this.confirm.emit();
  }
}
