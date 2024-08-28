import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-username-input',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './username-input.component.html',
  styleUrl: './username-input.component.css'
})
export class UsernameInputComponent {
  @Input() username = "";
  @Input() usernameError : string | null = null;
  @Output() usernameChange = new EventEmitter<string>();
  
  onUsernameChange(value: string){
    this.username = value;
    this.usernameChange.emit(value);
  }
}
