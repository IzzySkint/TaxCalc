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
        public List<SelectListItem>? PostalCodes { get; set; }
        public async Task OnGet()
        {
            logger.LogInformation("Starting calculator");

            try
            {
                var postalCodes = await calculationService.GetAllPostalCodesAsync();

                PostalCodes = postalCodes.Select(x => new SelectListItem(x.Code, x.Id.ToString())).ToList();

                PostalCodes.Insert(0, new SelectListItem("Select a postal code", ""));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting postal codes");
            }
            
        }
    }
}
