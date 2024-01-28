using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxCalc.Data.Entities;

namespace TaxCalc.Data.Configuration;

public class PostalCodeConfiguration : IEntityTypeConfiguration<PostalCode>
{
    public void Configure(EntityTypeBuilder<PostalCode> builder)
    {
        builder.ToTable("PostalCode");
        
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .IsRequired();
        
        builder.Property(x => x.Code)   
            .HasColumnName("Code")
            .HasColumnType("varchar(10)")
            .IsRequired();
        
        builder.Property(x => x.TaxCalculationTypeId)
            .HasColumnName("TaxCalculationTypeId")
            .HasColumnType("int")
            .IsRequired();
    }
}