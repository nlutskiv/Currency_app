using System.Text.Json;
using CurrencyConverterApi.Models;

namespace CurrencyConverterApi.Services;

public class CurrencyService
{
    private readonly HttpClient _httpClient;

    public CurrencyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> ConvertAsync(CurrencyConversionRequest request)
    {
        var from = request.From.ToUpper();
        var to = request.To.ToUpper();

        if (request.Amount < 0)
            throw new ArgumentException("Amount must be non-negative.");

        var url = $"https://api.frankfurter.dev/v1/latest?base={from}&symbols={to}";

        var json = await _httpClient.GetStringAsync(url);

        using var doc = JsonDocument.Parse(json);

        var rate = doc.RootElement
            .GetProperty("rates")
            .GetProperty(to)
            .GetDecimal();

        return request.Amount * rate;
    }
}