using NUnit.Framework;
using TaxCalc.Core.Enums;
using TaxCalc.Core.Services;
using TaxCalc.TaxCalculator.Calculators;
using TaxCalc.TaxCalculator.Exceptions;
using TaxCalc.TaxCalculator.Tests.Mocks;

namespace TaxCalc.TaxCalculator.Tests;

[TestFixture]
public class CalculatorTests
{
    private readonly TaxCalculatorFactory _factory;
    
    public CalculatorTests()
    {
        _factory = new TaxCalculatorFactory(new TaxCalculatorFactoryLogger(), new CalculatorServiceMock());
    }

    [Test]
    public async Task FlatValueTaxCalculationTest_Income_0()
    {
        var calculator = await _factory.CreateAsync(TaxCalculationTypes.FlatValue);
        Assert.Throws<CalculationException>(() => calculator.Calculate(0));
    }

    [Test]
    public async Task FlatValueTaxCalculationTest_Income_100000()
    {
        var calculator = await _factory.CreateAsync(TaxCalculationTypes.FlatValue);
        var expected = 100000 * 0.05m;
        var result = calculator.Calculate(100000);
        
        Assert.AreEqual(expected, result);
    }

    [Test]
    public async Task FlatValueTaxCalculationTest_Income_200000()
    {
        var calculator = await _factory.CreateAsync(TaxCalculationTypes.FlatValue);
        var expected = 10000;
        var result = calculator.Calculate(200000);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public async Task FlatRateTaxCalculationTest_Income_0()
    {
        var calculator = await _factory.CreateAsync(TaxCalculationTypes.FlatRate);
        Assert.Throws<CalculationException>(() => calculator.Calculate(0));
    }

    [Test]
    public async Task FlatRateTaxCalculationTest_Income_100000()
    {
        var expected = 100000 * 0.175m;

        var calculator = await _factory.CreateAsync(TaxCalculationTypes.FlatRate);
        var result = calculator.Calculate(100000);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public async Task FlatRateTaxCalculationTest_Income_200000()
    {
        var expected = 200000 * 0.175m;

        var calculator = await _factory.CreateAsync(TaxCalculationTypes.FlatRate);
        var result = calculator.Calculate(200000);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public async Task ProgressiveTaxCalculationTest_Income_0()
    {
        var calculator = await _factory.CreateAsync(TaxCalculationTypes.Progressive);
        Assert.Throws<CalculationException>(() => calculator.Calculate(0));
    }

    [Test]
    public async Task ProgressiveTaxCalculationTest_Income_100000()
    {
        var expected = CalculateProgressiveTax(100000);
        var calculator = await _factory.CreateAsync(TaxCalculationTypes.Progressive);

        var result = calculator.Calculate(100000);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public async Task ProgressiveTaxCalculationTest_Income_200000()
    {
        var expected = CalculateProgressiveTax(200000);

        var calculator = await _factory.CreateAsync(TaxCalculationTypes.Progressive);
        var result = calculator.Calculate(200000);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public async Task ProgressiveTaxCalculationTest_Income_1000000()
    {
        var expected = CalculateProgressiveTax(1000000);

        var calculator = await _factory.CreateAsync(TaxCalculationTypes.Progressive);
        var result = calculator.Calculate(1000000);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public async Task ProgressiveTaxCalculationTest_Income_1300000()
    {
        var expected = CalculateProgressiveTax(1300000);

        var calculator = await _factory.CreateAsync(TaxCalculationTypes.Progressive);
        var result = calculator.Calculate(1300000);

        Assert.AreEqual(expected, result);
    }

    private decimal CalculateProgressiveTax(decimal annualIncome)
    {
        decimal[] incomeThresholds = { 8350m, 33950m, 82250m, 171550m, 372950m, 3000000m };
        decimal[] taxRates = { 0.10m, 0.15m, 0.25m, 0.28m, 0.33m, 0.35m };

        decimal tax = 0;

        for (int i = 0; i < incomeThresholds.Length; i++)
        {
            if (annualIncome <= incomeThresholds[i])
            {
                tax += annualIncome * taxRates[i];
                break;
            }
            else
            {
                tax += (incomeThresholds[i] - (i == 0 ? 0 : incomeThresholds[i - 1])) * taxRates[i];
                annualIncome -= (incomeThresholds[i] - (i == 0 ? 0 : incomeThresholds[i - 1]));
            }
        }

        return tax;
    }
}