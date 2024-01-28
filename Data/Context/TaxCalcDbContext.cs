using Microsoft.EntityFrameworkCore;
using TaxCalc.Data.Configuration;
using TaxCalc.Data.Entities;

namespace TaxCalc.Data.Context;

public class TaxCalcDbContext : DbContext
{
    public TaxCalcDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Tax> Taxes { get; set; }
    public DbSet<TaxCalculation> TaxCalculations { get; set; }
    public DbSet<PostalCode> PostalCodes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaxConfiguration());
        modelBuilder.ApplyConfiguration(new TaxCalculationConfiguration());
        modelBuilder.ApplyConfiguration(new PostalCodeConfiguration());
    }
}