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
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error saving calculation, AnnualIncome: {calculation.AnnualIncome} PostalCodeId: {calculation.PostalCodeId} Result: {calculation.Result}");
            throw;
        }
        
    }

    public async Task<TaxCalculationTypes> GetCalculationTypeFromPostalCodeAsync(string postalCode)
    {
        try
        {
            logger.LogDebug($"Retrieving calculation type by postal code. Postal code: {postalCode}");
            PostalCode? postalCodeEntity = await unitOfWork.PostalCodes.GetByPostalCodeAsync(postalCode);

            if (postalCodeEntity == null)
            {
                return TaxCalculationTypes.Unknown;
            }

            return (TaxCalculationTypes)postalCodeEntity.TaxCalculationTypeId;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error retrieving calculation type by postal code. Postal code: {postalCode}");
            throw;
        }
        
    }

    public async Task<TaxCalculationTypes> GetCalculationTypeFromPostalCodeIdAsync(int id)
    {
        try
        {
            logger.LogDebug($"Retrieving calculation type by postal code id. Postal code id: {id}");
            PostalCode? postalCodeEntity = await unitOfWork.PostalCodes.GetByIdAsync(id);

            if (postalCodeEntity == null)
            {
                return TaxCalculationTypes.Unknown;
            }

            return (TaxCalculationTypes)(postalCodeEntity.TaxCalculationTypeId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error retrieving calculation type by postal code id. Postal code id: {id}");
            throw;
        }
        
    }

    public async Task<IEnumerable<Models.PostalCode>> GetAllPostalCodesAsync()
    {
        try
        {
            logger.LogDebug("Retrieving all postal codes");
            var postalCodes = await unitOfWork.PostalCodes.GetAllAsync();

            return mapper.Map<IEnumerable<Models.PostalCode>>(postalCodes);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving all postal codes");
            throw;
        }
        
    }
}