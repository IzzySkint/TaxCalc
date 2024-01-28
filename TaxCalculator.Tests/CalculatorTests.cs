using NUnit.Framework;
using TaxCalc.Core.Services;
using TaxCalc.TaxCalculator.Calculators;
using TaxCalc.TaxCalculator.Tests.Mocks;

namespace TaxCalc.TaxCalculator.Tests;

[TestFixture]
public class CalculatorTests
{
    private TaxCalculatorFactory _factory;
    
    public CalculatorTests()
    {
        _factory = new TaxCalculatorFactory(new CalculatorServiceMock());
    }
    
    
}