using System.Collections;
using System.Runtime.InteropServices.JavaScript;
using AutoMapper;
using TaxCalc.Core.Models;
using TaxCalc.Data.Entities;

namespace TaxCalc.Core.Mapping;

public class CoreProfile : Profile
{
    public CoreProfile()
    {
        CreateMap<Calculation, TaxCalculation>();
        CreateMap<Data.Entities.PostalCode, Models.PostalCode>();
        CreateMap<List<Tax>, TaxTable>().ConvertUsing(typeof(TaxTableConverter));
    }
}