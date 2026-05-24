# Currency Converter App

A full-stack currency converter built with Angular and ASP.NET Core.

The frontend allows users to select currencies and convert an amount to a target currency with the click of a button.
The backend communicates with the Frankfurter API for live conversion rates and handles the logic. 


## Tech Stack

Frontend:
- Angular
- TypeScript

Backend:
- ASP.NET Core Web API
- C#

Testing:
- xUnit


## Features

- Currency conversion using live exchange rates
- Angular frontend
- ASP.NET Core API backend
- Service layer architecture
- Error handling
- Unit tests with mocked HTTP responses


## Architecture

Frontend:
- Handles user interaction and displays results
- Sends HTTP requests to backend API

Backend:
- Validates requests
- Calls external exchange-rate API
- Parses JSON response
- Returns formatted conversion result

Testing:
- Unit tests isolate service logic using mocked HTTP handlers


## Running the Application

## Backend

```bash
cd CurrencyConverterApi
dotnet run
```

## Frontend

Open a second terminal:

```bash
cd currency-converter-frontend
npm install
ng serve
```

## Running Tests

```bash
cd CurrencyConverterApi.Tests
dotnet test
```

Frontend runs on:
http://localhost:4200

Backend runs on:
http://localhost:5072
