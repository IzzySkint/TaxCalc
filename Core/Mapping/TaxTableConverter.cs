using System.ComponentModel;
using AutoMapper;
using TaxCalc.Core.Enums;
using TaxCalc.Core.Models;
using TaxCalc.Data.Entities;

namespace TaxCalc.Core.Mapping;

public class TaxTableConverter : ITypeConverter<List<Tax>, TaxTable>
{
    public TaxTable Convert(List<Tax> source, TaxTable destination, ResolutionContext context)
    {
        TaxTable taxTable = new TaxTable();

        foreach (var tax in source)
        {
            var entry = new TaxTable.Entry
            {
                From = tax.From,
                To = tax.To,
                Rate = tax.Rate,
                Value = tax.Value,
                CalculationType = (TaxCalculationTypes)tax.TaxCalculationTypeId
            };

            taxTable.Entries.Add(entry);
        }

        return taxTable;
    }
}