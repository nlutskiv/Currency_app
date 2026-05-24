using System.Text.Json;
using CurrencyConverterApi.Models;

namespace CurrencyConverterApi.Services;

public class CurrencyService
{
    private readonly HttpClient _httpClient;

    public CurrencyService(HttpClient httpClient)
    {
        _httpClient = httpClient;       // For calling external APIs
    }

    public async Task<decimal> ConvertAsync(CurrencyConversionRequest request)
    {
        var from = request.From.ToUpper();      // "gbp" → "GBP"
        var to = request.To.ToUpper();          // "usd" → "USD"

        // Validation: amount can't be negative       
        if (request.Amount < 0)
            throw new ArgumentException("Amount must be non-negative.");

        // Validation: Currencies have to be different
        if (from == to)
            throw new ArgumentException("Source and destination currencies must be different.");

                
        if (request.Amount > 1000000000000000)
            throw new ArgumentException("Amount is too large.");
        
        // Build URL for Frankfurter free API
        var url = $"https://api.frankfurter.dev/v1/latest?base={from}&symbols={to}";
        // Example: https://api.frankfurter.dev/v1/latest?base=GBP&symbols=USD

        // Call Frankfurter API and get JSON response
        var json = await _httpClient.GetStringAsync(url);
        // Response looks like: { "rates": { "USD": 1.3145 } }

        // Parse the JSON
        using var doc = JsonDocument.Parse(json);

        // Extract the exchange rate from response
        var rate = doc.RootElement
            .GetProperty("rates")
            .GetProperty(to)
            .GetDecimal();

        // Return converted amount
        return request.Amount * rate;
    }
}