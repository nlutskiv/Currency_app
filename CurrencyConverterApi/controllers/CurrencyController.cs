using Microsoft.AspNetCore.Mvc;
using CurrencyConverterApi.Services;
using CurrencyConverterApi.Models;

namespace CurrencyConverterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyService _currencyService;

        public CurrencyController(CurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpPost("convert")]
        public async Task<IActionResult> Convert([FromBody] CurrencyConversionRequest request)
        {
            try
            {
                decimal convertedAmount = await _currencyService.ConvertAsync(
                    request
                );

                var response = new CurrencyConversionResponse
                {
                    From = request.From.ToUpper(),
                    To = request.To.ToUpper(),
                    Amount = request.Amount,
                    ConvertedAmount = Math.Round(convertedAmount, 2)
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}