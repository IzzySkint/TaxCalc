using TaxCalc.Core.Models;
using TaxCalc.TaxCalculator.Contracts;
using TaxCalc.TaxCalculator.Exceptions;

namespace TaxCalc.TaxCalculator.Calculators;

public class FlatValueCalculator : ITaxCalculator
{
    private readonly TaxTable _taxTable;
    
    public FlatValueCalculator(TaxTable taxTable)
    {
        _taxTable = taxTable;
    }
    
    public decimal Calculate(decimal annualIncome)
    {
        var entry = _taxTable.Entries.Where(x => annualIncome > x.From && annualIncome <= x.To)
            .Select(x => x)
            .FirstOrDefault();

        if (entry != null)
        {
            if (entry.Rate > 0)
            {
                return entry.Rate * annualIncome;
            }
            else
            {
                return entry.Value;
            }
        }
        else
        {
            throw new CalculationException($"No tax rate found for annual income {annualIncome})");
        }
    }
}