import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AlertModalService {
  modalIsOpen = false
  modalMessage = "Algo deu errado...";
  loading : boolean = true;

  constructor() {
    setTimeout(() => this.loading = false, 100)
  }

  toggleModal(){
    if (this.loading)
      return
    this.modalIsOpen = !this.modalIsOpen;
  }

  showModal() {
    if (this.loading)
      return
    this.modalIsOpen = true;
  }
}
