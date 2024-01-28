using TaxCalc.Core.Contracts;
using TaxCalc.Core.Enums;
using TaxCalc.Core.Models;

namespace TaxCalc.TaxCalculator.Tests.Mocks;

public class CalculatorServiceMock : ICalculatorService
{
    public CalculatorServiceMock()
    {
    }
    public async Task<TaxTable> GetTaxTableAsync(TaxCalculationTypes calculationType)
    {
        TaxTable taxTable = null;
        
        switch (calculationType)
        {
            case TaxCalculationTypes.FlatRate:
                taxTable = await Task.Run(() => GetFlatRateTaxTable());
                break;
            case TaxCalculationTypes.FlatValue:
                taxTable = await Task.Run(() => GetFlatValueTaxTable());
                break;
            case TaxCalculationTypes.Progressive:
                taxTable = await Task.Run(() => GetProgressiveTaxTable());
                break;
        }

        return taxTable;
    }
    
    private TaxTable GetFlatValueTaxTable()
    {
        TaxTable taxTable = new TaxTable();

        return null;
    }
    
    private TaxTable GetFlatRateTaxTable()
    {
        TaxTable taxTable = new TaxTable();

        return null;
    }
    
    private TaxTable GetProgressiveTaxTable()
    {
        TaxTable taxTable = new TaxTable();

        return null;
    }
}