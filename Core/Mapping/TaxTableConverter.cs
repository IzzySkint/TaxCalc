using AutoMapper;
using TaxCalc.Core.Enums;
using TaxCalc.Core.Models;
using TaxCalc.Data.Entities;

namespace TaxCalc.Core.Mapping;

public class TaxTableConverter : ITypeConverter<IEnumerable<Tax>, TaxTable>
{
    public TaxTable Convert(IEnumerable<Tax> source, TaxTable destination, ResolutionContext context)
    {
        var taxTable = new TaxTable();
        foreach (var tax in source)
        {
            taxTable.Entries.Add(new TaxTable.Entry
            {
                From = tax.From,
                To = tax.To,
                Rate = tax.Rate,
                Value = tax.Value,
                CalculationType = (TaxCalculationTypes) tax.TaxCalculationTypeId
            });
        }

        return taxTable;
    }
}