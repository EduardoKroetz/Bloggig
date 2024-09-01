import { HttpClient } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { error } from 'console';
import { AlertModalService } from '../../services/alert-modal.service';

@Component({
  selector: 'app-logout-button',
  standalone: true,
  imports: [],
  templateUrl: './logout-button.component.html',
  styleUrl: './logout-button.component.css'
})
export class LogoutButtonComponent {

  constructor (private authService: AuthService, private alertService: AlertModalService ) {}

  handleLogout(){
    this.authService.logOut().subscribe(
      (res) => {
        window.location.href = "/auth/login"
      },
      (error) => {
        this.alertService.modalIsOpen = true;
        this.alertService.modalMessage = "Não foi possível sair da conta"
      }
    );
  }
}
