using System.Net;
using CurrencyConverterApi.Models;
using CurrencyConverterApi.Services;

namespace CurrencyConverterApi.Tests;

public class CurrencyServiceTests
{
    [Fact]
    public async Task ConvertAsync_WithValidRequest_ReturnsConvertedAmount()
    {
        var json = """
        {
            "amount": 1.0,
            "base": "GBP",
            "date": "2024-01-01",
            "rates": {
                "USD": 1.25
            }
        }
        """;
        // Fake HTTP handler used to simulate external API responses
        var httpClient = new HttpClient(new FakeHttpMessageHandler(json));
        var service = new CurrencyService(httpClient);

        var request = new CurrencyConversionRequest
        {
            From = "GBP",
            To = "USD",
            Amount = 100
        };

        var result = await service.ConvertAsync(request);
        // Verify conversion result is calculated correctly
        Assert.Equal(125, result);
    }

    [Fact]
    public async Task ConvertAsync_WithNegativeAmount_ThrowsArgumentException()
    {
        var httpClient = new HttpClient(new FakeHttpMessageHandler("{}"));
        // Inject mocked HttpClient into service
        var service = new CurrencyService(httpClient);

        var request = new CurrencyConversionRequest
        {
            From = "GBP",
            To = "USD",
            Amount = -10
        };
        // Verify invalid input throws expected exception
        await Assert.ThrowsAsync<ArgumentException>(() => service.ConvertAsync(request));
    }

    [Fact]
    public async Task ConvertAsync_NormalisesLowercaseCurrencyCodes()
    {
        var json = """
        {
            "rates": {
                "USD": 1.5
            }
        }
        """;

        var httpClient = new HttpClient(new FakeHttpMessageHandler(json));
        var service = new CurrencyService(httpClient);

        var request = new CurrencyConversionRequest
        {
            From = "gbp",
            To = "usd",
            Amount = 20
        };

        var result = await service.ConvertAsync(request);

        Assert.Equal(30, result);
    }
}

public class FakeHttpMessageHandler : HttpMessageHandler
{
    private readonly string _response;

    public FakeHttpMessageHandler(string response)
    {
        _response = response;
    }

    // Override SendAsync to intercept outgoing HTTP requests
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(_response)
        };
        // Return completed async task containing fake HTTP response
        return Task.FromResult(response);
    }
}