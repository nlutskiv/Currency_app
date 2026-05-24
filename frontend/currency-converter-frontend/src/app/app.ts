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

  currencies = ['GBP', 'USD', 'EUR', 'JPY'];

  from = 'GBP';
  to = 'USD';
  amount = 100;

  result: any = null;
  error = '';

  constructor(
    private http: HttpClient,
    private cdr: ChangeDetectorRef
  ) {}

  convert() {

    this.error = '';

    const request = {
      from: this.from,
      to: this.to,
      amount: this.amount
    };

    this.http.post(
      'http://localhost:5072/api/currency/convert',
      request
    )
    .subscribe({
      next: (response: any) => {

        console.log(response);

        this.result = response;

        this.cdr.detectChanges();
      },

      error: (err) => {

        this.error = err.error?.error || 'Conversion failed';

        this.cdr.detectChanges();
      }
    });
  }
}