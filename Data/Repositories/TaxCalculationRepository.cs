using TaxCalc.Data.Context;
using TaxCalc.Data.Contracts;
using TaxCalc.Data.Entities;

namespace TaxCalc.Data.Repositories;

public class TaxCalculationRepository : Repository<TaxCalculation>, ITaxCalculationRepository
{
    public TaxCalculationRepository(TaxCalcDbContext context) : base(context)
    {
    }
    
    private TaxCalcDbContext Context => _context as TaxCalcDbContext;
}