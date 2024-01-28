using TaxCalc.Data.Context;
using TaxCalc.Data.Contracts;

namespace TaxCalc.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly TaxCalcDbContext _context;
    
    public UnitOfWork(TaxCalcDbContext context)
    {
        _context = context;
        TaxCalculations = new TaxCalculationRepository(_context);
        Taxes = new TaxRepository(_context);
        PostalCodes = new PostalCodeRepository(_context);
    }
    
    public ITaxCalculationRepository TaxCalculations { get; private set; }
    public ITaxRepository Taxes { get; private set; }
    public IPostalCodeRepository PostalCodes { get; private set; }
    
    public async Task<int> CompleteAsync()
    {
        var result = await _context.SaveChangesAsync();
        return result;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}