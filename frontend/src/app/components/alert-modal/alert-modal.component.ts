import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AlertModalService } from '../../services/alert-modal.service';

@Component({
  selector: 'app-alert-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './alert-modal.component.html',
  styleUrl: './alert-modal.component.css'
})
export class AlertModalComponent {
  constructor (public alertModalService: AlertModalService) {}

  closeModalOnClickOutside(event: MouseEvent) {
    if (event.target === event.currentTarget) {
      this.alertModalService.toggleModal();
    }
  }
}
