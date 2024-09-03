import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-search-form',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './search-form.component.html',
  styleUrl: './search-form.component.css'
})
export class SearchFormComponent {
  reference = "";

  constructor (private router: Router) {}

  handleSubmit(event: SubmitEvent) {
    event.preventDefault();

    this.router.navigate(["/search"], {
      queryParams: { reference: this.reference }
    });
  }

}
