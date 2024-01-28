namespace TaxCalc.Data.Contracts;

public interface IUnitOfWork : IDisposable
{
    ITaxCalculationRepository TaxCalculations { get; }
    ITaxRepository Taxes { get; }
    IPostalCodeRepository PostalCodes { get; }
    Task<int> CompleteAsync();
}