import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-email-input',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './email-input.component.html',
  styleUrl: './email-input.component.css'
})
export class EmailInputComponent {
  @Input() email: string = "";
  @Input() errorEmail : string | null = null;
  @Output() emailChange = new EventEmitter<string>();

  onEmailChange(value: string) {
    this.email = value;
    this.emailChange.emit(value); 
  }
}
