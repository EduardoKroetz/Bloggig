import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AlertModalService {
  modalIsOpen = false
  modalMessage = "Algo deu errado...";

  constructor() { }

  toggleModal(){
    this.modalIsOpen = !this.modalIsOpen;
  }
}
