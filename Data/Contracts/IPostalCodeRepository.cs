using TaxCalc.Data.Entities;

namespace TaxCalc.Data.Contracts;

public interface IPostalCodeRepository : IRepository<PostalCode>
{
    Task<PostalCode> GetByPostalCodeAsync(string postalCode);
}