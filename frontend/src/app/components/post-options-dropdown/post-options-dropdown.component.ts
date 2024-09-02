import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-post-options-dropdown',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './post-options-dropdown.component.html',
  styleUrl: './post-options-dropdown.component.css'
})
export class PostOptionsDropdownComponent {
  @Input() dropdownIsOpen = false;
  @Output() closedropdown = new EventEmitter<any>();

  onCloseDropdown() {
    this.dropdownIsOpen = false;
    this.closedropdown.emit();
  }
}
