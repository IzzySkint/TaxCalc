namespace TaxCalc.Data.Entities;

public class TaxCalculation
{
    public long Id { get; set; }
    public int PostalCodeId { get; set; }
    public decimal AnnualIncome { get; set; }
    public decimal Result { get; set; }
}