using AutoMapper;
using TaxCalc.Core.Contracts;
using TaxCalc.Core.Enums;
using TaxCalc.Core.Models;
using TaxCalc.Data.Contracts;
using TaxCalc.Data.Entities;
using PostalCode = TaxCalc.Data.Entities.PostalCode;
using Microsoft.Extensions.Logging;

namespace TaxCalc.Core.Services;

public class CalculationService(ILogger<CalculationService> logger, IUnitOfWork unitOfWork, IMapper mapper) : ICalculationService
{
    public async Task SaveCalculationAsync(Calculation calculation)
    {
        logger.LogInformation($"SaveCalculationAsync with calculation, AnnualIncome: {calculation.AnnualIncome} PostalCodeId: {calculation.PostalCodeId} Result: {calculation.Result}");

        try
        {
            var taxCalculation = mapper.Map<TaxCalculation>(calculation);

            await unitOfWork.TaxCalculations.AddAsync(taxCalculation);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public async Task<TaxCalculationTypes> GetCalculationTypeFromPostalCodeAsync(string postalCode)
    {
        PostalCode? postalCodeEntity = await unitOfWork.PostalCodes.GetByPostalCodeAsync(postalCode);
        
        if (postalCodeEntity == null)
        {
            return TaxCalculationTypes.Unknown;
        }
        
        return (TaxCalculationTypes) postalCodeEntity.TaxCalculationTypeId;
    }

    public async Task<TaxCalculationTypes> GetCalculationTypeFromPostalCodeIdAsync(int id)
    {
        PostalCode? postalCodeEntity = await unitOfWork.PostalCodes.GetByIdAsync(id);

        if (postalCodeEntity == null)
        {
            return TaxCalculationTypes.Unknown;
        }

        return (TaxCalculationTypes) (postalCodeEntity.TaxCalculationTypeId);
    }

    public async Task<IEnumerable<Models.PostalCode>> GetAllPostalCodesAsync()
    {
        var postalCodes = await unitOfWork.PostalCodes.GetAllAsync();
        
        return mapper.Map<IEnumerable<Models.PostalCode>>(postalCodes);
    }
}