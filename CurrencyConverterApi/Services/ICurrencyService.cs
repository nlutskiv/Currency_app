using CurrencyConverterApi.Models;

namespace CurrencyConverterApi.Services;

public interface ICurrencyService
{
    Task<decimal> ConvertAsync(CurrencyConversionRequest request);
}