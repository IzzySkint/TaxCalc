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
        TaxTable? taxTable = null;
        
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
            default:
                throw new ArgumentException($"Unsupported calculation type, {calculationType}");
        }

        return taxTable;
    }
    
    private TaxTable GetFlatValueTaxTable()
    {
        TaxTable taxTable = new TaxTable();

        taxTable.Entries.Add(new TaxTable.Entry
        {
            From = 0,
            To = 199999,
            Rate = 0.05m,
            Value = 0,
            CalculationType = TaxCalculationTypes.FlatValue
        });

        taxTable.Entries.Add(new TaxTable.Entry
        {
            From = 200000,
            To = 3000000,
            Rate = 0.0m,
            Value = 10000,
            CalculationType = TaxCalculationTypes.FlatValue
        }); 

        return taxTable;
    }
    
    private TaxTable GetFlatRateTaxTable()
    {
        TaxTable taxTable = new TaxTable();

        taxTable.Entries.Add(new TaxTable.Entry
        {
            From = 0,
            To = 3000000,
            Rate = 0.175m,
            Value = 0,
            CalculationType = TaxCalculationTypes.FlatRate
        });

        return taxTable;
    }
    
    private TaxTable GetProgressiveTaxTable()
    {
        TaxTable taxTable = new TaxTable();

        taxTable.Entries.Add(new TaxTable.Entry
        {
            From = 0,
            To = 8350,
            Rate = 0.10m,
            Value = 0,
            CalculationType = TaxCalculationTypes.Progressive
        });

        taxTable.Entries.Add(new TaxTable.Entry
        {
            From = 8351,
            To = 33950,
            Rate = 0.15m,
            Value = 0,
            CalculationType = TaxCalculationTypes.Progressive
        });

        taxTable.Entries.Add(new TaxTable.Entry
        {
            From = 33951,
            To = 82250,
            Rate = 0.25m,
            Value = 0,
            CalculationType = TaxCalculationTypes.Progressive
        });

        taxTable.Entries.Add(new TaxTable.Entry
        {
            From = 82251,
            To = 171550,
            Rate = 0.28m,
            Value = 0,
            CalculationType = TaxCalculationTypes.Progressive
        });

        taxTable.Entries.Add(new TaxTable.Entry
        {
            From = 171551,
            To = 372950,
            Rate = 0.33m,
            Value = 0,
            CalculationType = TaxCalculationTypes.Progressive
        });

        taxTable.Entries.Add(new TaxTable.Entry
        {
            From = 372951,
            To = 3000000,
            Rate = 0.35m,
            Value = 0,
            CalculationType = TaxCalculationTypes.Progressive
        });

        return taxTable;
    }
}