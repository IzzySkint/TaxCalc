namespace TaxCalc.Data.Entities;

public class Tax
{
    public int Id { get; set; }
    public decimal From { get; set; }
    public decimal To { get; set; }
    public decimal Rate { get; set; }
    public decimal Value { get; set; }
    public int TaxCalculationTypeId { get; set; }
}