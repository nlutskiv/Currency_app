import { Component, ChangeDetectorRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  // Available currencies user can pick from
  currencies = ['GBP', 'USD', 'EUR', 'JPY'];

  // What the user selects/enters
  from = 'GBP';       // Source currency (default)
  to = 'USD';         // Target currency (default)
  amount = 100;       // Amount to convert (default)

  // Stores the API response
  result: any = null;
  error = '';         // If something goes wrong

  constructor(
    private http: HttpClient,         // For making API calls
    private cdr: ChangeDetectorRef    // For updating the UI
  ) {}

  convert() {

    this.error = '';        // Clear any previous errors
    this.result = null;


    // Create the request object with user's choices
    const request = {
      from: this.from,
      to: this.to,
      amount: this.amount
    };


    // Make POST request to beckend API
    this.http.post(
      'http://localhost:5072/api/currency/convert',
      request
    )
    .subscribe({
      // If request succeeds
      next: (response: any) => {

        console.log(response);

        this.result = response;     // Store the result

        this.cdr.detectChanges();   // Tell Angular to update the UI
      },
      // If request fails
      error: (err) => {

        this.error = err.error?.error || 'Conversion failed';

        this.cdr.detectChanges();
      }
    });
  }
}