using Microsoft.AspNetCore.Mvc;
using CurrencyConverterApi.Services;
using CurrencyConverterApi.Models;

namespace CurrencyConverterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]         // URL: /api/currency/...
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpPost("convert")]   // Endpoint: POST /api/currency/convert
        public async Task<IActionResult> Convert([FromBody] CurrencyConversionRequest request)
        {
            try
            {
                // do the conversion
                decimal convertedAmount = await _currencyService.ConvertAsync(
                    request
                );

                // Build response object
                var response = new CurrencyConversionResponse
                {
                    From = request.From.ToUpper(),      // "gbp" - "GBP"
                    To = request.To.ToUpper(),          // "usd" - "USD"
                    Amount = request.Amount,
                    ConvertedAmount = Math.Round(convertedAmount, 2)    // Round to 2 decimals
                };

                return Ok(response);        // HTTP 200 + JSON response
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });   // HTTP 400 + error message
            }
        }
    }
}