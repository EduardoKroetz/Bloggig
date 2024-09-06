import { Component } from '@angular/core';
import { ProfileImageFormComponent } from "../../components/profile-image-form/profile-image-form.component";
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-profile-image',
  standalone: true,
  imports: [ProfileImageFormComponent, RouterLink],
  templateUrl: './profile-image.component.html',
  styleUrl: './profile-image.component.css'
})
export class ProfileImageComponent {
  
}
