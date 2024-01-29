using TaxCalc.Core.Models;
using TaxCalc.TaxCalculator.Contracts;
using TaxCalc.TaxCalculator.Exceptions;

namespace TaxCalc.TaxCalculator.Calculators;

public class FlatRateCalculator : ITaxCalculator
{
    private readonly TaxTable _taxTable;
    public FlatRateCalculator(TaxTable taxTable)
    {
        _taxTable = taxTable;
    }
    
    public decimal Calculate(decimal annualIncome)
    {
        if (annualIncome <= 0)
        {
            throw new CalculationException($"Annual income must be greater than 0 ({annualIncome})");
        }

        var entry = _taxTable.Entries.Where(x => annualIncome >= x.From && annualIncome <= x.To)
            .Select(x => x)
            .FirstOrDefault();

        if (entry != null)
        {
            return entry.Rate * annualIncome;
        }
        else
        {
            throw new CalculationException($"No tax rate found for annual income {annualIncome})");
        }
    }
}