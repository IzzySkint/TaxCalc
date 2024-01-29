using TaxCalc.Core.Contracts;
using TaxCalc.Core.Enums;
using TaxCalc.TaxCalculator.Contracts;
using Microsoft.Extensions.Logging;
using TaxCalc.Core.Models;

namespace TaxCalc.TaxCalculator.Calculators;

public class TaxCalculatorFactory(ILogger<TaxCalculatorFactory> logger, ICalculatorService calculatorService) : ITaxCalculatorFactory
{
    public async Task<ITaxCalculator> CreateAsync(TaxCalculationTypes calculationType)
    {
        TaxTable? taxTable = null;

        try
        {
            logger.LogInformation($"CreateAsync with calculationType: {calculationType}");
            taxTable = await calculatorService.GetTaxTableAsync(calculationType);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error getting tax table for calculation type {calculationType}");
            throw;
        }

        return calculationType switch
        {
            TaxCalculationTypes.FlatValue => new FlatValueCalculator(taxTable),
            TaxCalculationTypes.FlatRate => new FlatRateCalculator(taxTable),
            TaxCalculationTypes.Progressive => new ProgressiveCalculator(taxTable),
            _ => throw new ArgumentOutOfRangeException(nameof(calculationType), calculationType, "Unsupported calculator type")
        };
    }
}