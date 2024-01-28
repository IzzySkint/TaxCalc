

using Microsoft.EntityFrameworkCore;
using TaxCalc.Data.Context;
using TaxCalc.Data.Contracts;
using TaxCalc.Data.Entities;

namespace TaxCalc.Data.Repositories;

public class PostalCodeRepository : Repository<PostalCode>, IPostalCodeRepository
{
    public PostalCodeRepository(TaxCalcDbContext context) : base(context)
    {
    }
    
    public async Task<PostalCode> GetByPostalCodeAsync(string postalCode)
    {
        return await Context.PostalCodes
            .FirstOrDefaultAsync(p => p.Code == postalCode);
    }
    
    private TaxCalcDbContext Context => _context as TaxCalcDbContext;
    
}