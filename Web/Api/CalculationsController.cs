using Microsoft.AspNetCore.Mvc;
using TaxCalc.Core.Contracts;
using TaxCalc.Core.Enums;
using TaxCalc.Core.Models;
using TaxCalc.TaxCalculator.Contracts;
using TaxCalc.Web.Models;

namespace TaxCalc.Web.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculationsController(ILogger<CalculationsController> logger, ICalculationService calculationService, ITaxCalculatorFactory calculatorFactory)
        : Controller
    {

        [HttpPost]
        [Route("calculate")]
        public async Task<IActionResult> Calculate([FromBody] CalculationRequest request)
        {
            if (request.PostalCodeId == 0)
            {
                return BadRequest("Postal code is required");
            }

            if (request.AnnualIncome == 0)
            {
                return BadRequest("Annual income is required");
            }

            try
            {
                var calculationType = await calculationService.GetCalculationTypeFromPostalCodeIdAsync(request.PostalCodeId);

                if (calculationType == TaxCalculationTypes.Unknown)
                {
                    return BadRequest($"Postal code {request.PostalCodeId} is not supported");
                }

                var calculator = await calculatorFactory.CreateAsync(calculationType);
                var result = calculator.Calculate(request.AnnualIncome);

                var calculation = new Calculation
                {
                    AnnualIncome = request.AnnualIncome,
                    PostalCodeId = request.PostalCodeId,
                    Result = result
                };

                await calculationService.SaveCalculationAsync(calculation);

                var response = new CalculationResponse
                {
                    TaxResult = result.ToString("#.##")
                };

                return Json(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error calculating tax, with annualIncome: {request.AnnualIncome}, postal code id: {request.PostalCodeId}");
                return StatusCode(500, "Error calculating tax");
            }
        }
    }
}
