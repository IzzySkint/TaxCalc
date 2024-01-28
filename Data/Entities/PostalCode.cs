namespace TaxCalc.Data.Entities;

public class PostalCode
{
    public int Id { get; set; }
    public string Code { get; set; }
    public int TaxCalculationTypeId { get; set; }
}