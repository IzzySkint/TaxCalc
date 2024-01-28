using TaxCalc.Core.Models;
using TaxCalc.TaxCalculator.Contracts;
using TaxCalc.TaxCalculator.Exceptions;

namespace TaxCalc.TaxCalculator.Calculators;

public class ProgressiveCalculator : ITaxCalculator
{
    private readonly TaxTable _taxTable;
    
    public ProgressiveCalculator(TaxTable taxTable)
    {
        _taxTable = taxTable;
    }
    
    public decimal Calculate(decimal annualIncome)
    {
        decimal[] thresholds = _taxTable.Entries.Select(x => x.To).ToArray();
        decimal[] rates = _taxTable.Entries.Select(x => x.Rate).ToArray();
        
        var entry = _taxTable.Entries.Where(x => annualIncome > x.From && annualIncome <= x.To)
            .Select(x => x)
            .FirstOrDefault();

        if (entry == null)
        {
            throw new CalculationException($"Annual income {annualIncome} is not within the tax table range");
        }

        decimal tax = 0;
        
        for (int i = 0; i < thresholds.Length - 1; i++)
        {
            if (annualIncome <= thresholds[i])
            {
                tax += annualIncome * rates[i];
                break;
            }
            else
            {
                tax += (thresholds[i + 1] - thresholds[i]) * rates[i];
                annualIncome -= (thresholds[i + 1] - thresholds[i]);
            }
        }

        return tax;
    }
}