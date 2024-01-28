using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxCalc.Data.Entities;

namespace TaxCalc.Data.Configuration;

internal sealed class TaxConfiguration : IEntityTypeConfiguration<Tax>
{
    public void Configure(EntityTypeBuilder<Tax> builder)
    {
        builder.ToTable("Tax");
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.From)
            .HasColumnName("From")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.To)
            .HasColumnName("To")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.Rate)
            .HasColumnName("Rate")
            .HasColumnType("decimal(18,3)")
            .IsRequired();

        builder.Property(x => x.Value)
            .HasColumnName("Value")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.TaxCalculationTypeId)
            .HasColumnName("TaxCalculationTypeId")
            .HasColumnType("int")
            .IsRequired();

    }
}