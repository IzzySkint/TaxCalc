using AutoMapper;
using Microsoft.Extensions.Logging;
using TaxCalc.Core.Contracts;
using TaxCalc.Core.Enums;
using TaxCalc.Core.Models;
using TaxCalc.Data.Contracts;

namespace TaxCalc.Core.Services;

public class CalculatorService(ILogger<CalculationService> logger, IUnitOfWork unitOfWork, IMapper mapper) : ICalculatorService
{
    public async Task<TaxTable> GetTaxTableAsync(TaxCalculationTypes calculationType)
    {
        try
        {
            logger.LogDebug($"Retrieving tax table by calculation type. Calculation type: {calculationType}");
            var taxes = await unitOfWork.Taxes.FindAsync(x => x.TaxCalculationTypeId == (int)calculationType);

            var taxTable = mapper.Map<TaxTable>(taxes);

            return taxTable;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error retrieving tax table by calculation type. Calculation type: {calculationType}");
            throw;
        }
        

    }
}