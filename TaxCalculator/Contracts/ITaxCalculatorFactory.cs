using TaxCalc.Core.Enums;

namespace TaxCalc.TaxCalculator.Contracts;

public interface ITaxCalculatorFactory
{
    Task<ITaxCalculator> CreateAsync(TaxCalculationTypes calculationType);
}