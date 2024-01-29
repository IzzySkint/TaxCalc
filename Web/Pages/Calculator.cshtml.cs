using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaxCalc.Core.Contracts;
using TaxCalc.Data.Contracts;

namespace TaxCalc.Web.Pages
{
    public class CalculatorModel(ICalculationService calculationService) : PageModel
    {
        private readonly ICalculationService _calculationService = calculationService;

        public List<SelectListItem>? PostalCodes { get; set; }
        public async Task OnGet()
        {
            var postalCodes = await _calculationService.GetAllPostalCodesAsync();

            PostalCodes = postalCodes.Select(x => new SelectListItem(x.Code, x.Id.ToString())).ToList();

            PostalCodes.Insert(0, new SelectListItem("Select a postal code", ""));
        }
    }
}
