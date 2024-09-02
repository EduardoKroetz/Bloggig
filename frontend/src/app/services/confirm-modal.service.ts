import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfirmModalService {
  modalIsOpen = false
  modalMessage = "Algo deu errado...";
  
  constructor() { }

  toggleModal(){
    this.modalIsOpen = !this.modalIsOpen;
  }
}
