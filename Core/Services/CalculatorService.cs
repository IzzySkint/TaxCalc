using AutoMapper;
using TaxCalc.Core.Contracts;
using TaxCalc.Core.Enums;
using TaxCalc.Core.Models;
using TaxCalc.Data.Contracts;

namespace TaxCalc.Core.Services;

public class CalculatorService(IUnitOfWork unitOfWork, IMapper mapper) : ICalculatorService
{
    public async Task<TaxTable> GetTaxTableAsync(TaxCalculationTypes calculationType)
    {
        var taxes = await unitOfWork.Taxes.FindAsync(x => x.TaxCalculationTypeId == (int)calculationType);
        var taxTable = mapper.Map<TaxTable>(taxes);

        return taxTable;
    }
}