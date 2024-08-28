import { Component } from '@angular/core';
import { ErrorModalService } from '../../services/error-modal.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-error-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './error-modal.component.html',
  styleUrl: './error-modal.component.css'
})
export class ErrorModalComponent {
  constructor (public errorModalService: ErrorModalService) {}

  closeModalOnClickOutside(event: MouseEvent) {
    if (event.target === event.currentTarget) {
      this.errorModalService.toggleModal();
    }
  }
}
