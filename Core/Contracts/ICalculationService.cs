using TaxCalc.Core.Enums;
using TaxCalc.Core.Models;

namespace TaxCalc.Core.Contracts;

public interface ICalculationService
{
    Task SaveCalculationAsync(Calculation calculation);
    Task<TaxCalculationTypes> GetCalculationTypeFromPostalCodeAsync(string postalCode);
    Task<TaxCalculationTypes> GetCalculationTypeFromPostalCodeIdAsync(int id);
   
    Task<IEnumerable<PostalCode>> GetAllPostalCodesAsync();
}