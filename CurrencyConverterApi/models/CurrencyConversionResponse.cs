namespace CurrencyConverterApi.Models;

// What goes OUT back to Angular
public class CurrencyConversionResponse
{
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal ConvertedAmount { get; set; }
}

// What comes IN from Angular
public class CurrencyConversionRequest
{
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}