import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { UserProfileImgComponent } from "../../../../components/user-profile-img/user-profile-img.component";
import { CommonModule } from '@angular/common';
import User from '../../../../interfaces/User';

@Component({
  selector: 'app-user-result',
  standalone: true,
  imports: [RouterLink, UserProfileImgComponent, CommonModule],
  templateUrl: './user-result.component.html',
  styleUrl: './user-result.component.css'
})
export class UserResultComponent {
  @Input() user : User | null = null;
}
