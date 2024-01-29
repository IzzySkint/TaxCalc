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
    public class CalculationsController(ICalculationService calculationService, ITaxCalculatorFactory calculatorFactory)
        : Controller
    {
        private readonly ICalculationService _calculationService = calculationService;
        private readonly ITaxCalculatorFactory _calculatorFactory = calculatorFactory;

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

            var calculationType = await _calculationService.GetCalculationTypeFromPostalCodeIdAsync(request.PostalCodeId);

            if (calculationType == TaxCalculationTypes.Unknown)
            {
                return BadRequest($"Postal code {request.PostalCodeId} is not supported");
            }

            var calculator = await _calculatorFactory.CreateAsync(calculationType);
            var result = calculator.Calculate(request.AnnualIncome);

            var calculation = new Calculation
            {
                AnnualIncome = request.AnnualIncome,
                PostalCodeId = request.PostalCodeId,
                Result = result
            };

            await _calculationService.SaveCalculationAsync(calculation);

            var response = new CalculationResponse
            {
                TaxResult = result.ToString("#.##")
            };

            return Json(response);
        }
    }
}
