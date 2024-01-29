using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaxCalc.Core.Contracts;
using TaxCalc.Data.Contracts;
using Microsoft.Extensions.Logging;


namespace TaxCalc.Web.Pages
{
    public class CalculatorModel(ILogger<CalculatorModel> logger, ICalculationService calculationService) : PageModel
    {
        private readonly ICalculationService _calculationService = calculationService;
        private readonly ILogger _logger = logger;

        public List<SelectListItem>? PostalCodes { get; set; }
        public async Task OnGet()
        {
            _logger.LogInformation("OnGet");
            var postalCodes = await _calculationService.GetAllPostalCodesAsync();

            PostalCodes = postalCodes.Select(x => new SelectListItem(x.Code, x.Id.ToString())).ToList();

            PostalCodes.Insert(0, new SelectListItem("Select a postal code", ""));
        }
    }
}
