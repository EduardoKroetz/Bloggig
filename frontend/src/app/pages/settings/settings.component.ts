import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserSettingsInfoComponent } from "../../components/user-settings-info/user-settings-info.component";

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [CommonModule, UserSettingsInfoComponent],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.css'
})
export class SettingsComponent {
  section : 'info' | 'posts' = 'info';
}
