using TaxCalc.Core.Enums;
using TaxCalc.Core.Models;

namespace TaxCalc.Core.Contracts;

public interface ICalculatorService
{
    Task<TaxTable> GetTaxTableAsync(TaxCalculationTypes calculationType);
}