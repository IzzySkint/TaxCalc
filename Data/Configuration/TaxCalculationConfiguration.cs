using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxCalc.Data.Entities;

namespace TaxCalc.Data.Configuration;

public class TaxCalculationConfiguration : IEntityTypeConfiguration<TaxCalculation>
{
    public void Configure(EntityTypeBuilder<TaxCalculation> builder)
    {
        builder.ToTable("TaxCalculation");
        
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("bigint")
            .IsRequired();
        
        builder.Property(x => x.PostalCodeId)
            .HasColumnName("PostalCodeId")
            .HasColumnType("int")
            .IsRequired();
        
        builder.Property(x => x.AnnualIncome)
            .HasColumnName("AnnualIncome")
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        
        builder.Property(x => x.Result)
            .HasColumnName("Result")
            .HasColumnType("decimal(18,2)")
            .IsRequired();
    }
}