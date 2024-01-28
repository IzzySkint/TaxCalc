using TaxCalc.Core.Contracts;
using TaxCalc.Core.Enums;
using TaxCalc.TaxCalculator.Contracts;

namespace TaxCalc.TaxCalculator.Calculators;

public class TaxCalculatorFactory : ITaxCalculatorFactory
{
    private readonly ICalculatorService _calculatorService;
    
    public TaxCalculatorFactory(ICalculatorService calculatorService)
    {
        _calculatorService = calculatorService;
    }

    public async Task<ITaxCalculator> CreateAsync(TaxCalculationTypes calculationType)
    {
        var taxTable = await _calculatorService.GetTaxTableAsync(calculationType);
        return calculationType switch
        {
            TaxCalculationTypes.FlatValue => new FlatValueCalculator(taxTable),
            TaxCalculationTypes.FlatRate => new FlatRateCalculator(taxTable),
            TaxCalculationTypes.Progressive => new ProgressiveCalculator(taxTable),
            _ => throw new ArgumentOutOfRangeException(nameof(calculationType), calculationType, "Unsupported calculator type")
        };
    }
}