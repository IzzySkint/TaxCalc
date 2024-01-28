namespace TaxCalc.TaxCalculator.Contracts;

public interface ITaxCalculator
{
    decimal Calculate(decimal annualIncome);
}