import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ErrorModalService {
  modalIsOpen = false
  modalMessage = "Algo deu errado...";

  constructor() { }

  toggleModal(){
    this.modalIsOpen = !this.modalIsOpen;
  }
}
