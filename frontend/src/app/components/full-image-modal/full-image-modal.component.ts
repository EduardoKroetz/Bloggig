import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CloseIconComponent } from "../close-icon/close-icon.component";

@Component({
  selector: 'app-full-image-modal',
  standalone: true,
  imports: [CommonModule, CloseIconComponent],
  templateUrl: './full-image-modal.component.html',
  styleUrl: './full-image-modal.component.css'
})
export class FullImageModalComponent {
  @Input() imageUrl = "";
  @Input() fullImageIsOpen = true;
  @Output() closeFullImage = new EventEmitter<any>();

  closeModal(){
    this.closeFullImage.emit();
  }

  closeModalOnClickOutside(event: MouseEvent) {
    if (event.target === event.currentTarget) {
      this.closeModal();
    }
  }
}
