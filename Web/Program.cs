using Microsoft.EntityFrameworkCore;
using TaxCalc.Core.Contracts;
using TaxCalc.Core.Mapping;
using TaxCalc.Core.Services;
using TaxCalc.Data.Context;
using TaxCalc.Data.Contracts;
using TaxCalc.Data.Repositories;
using TaxCalc.TaxCalculator.Calculators;
using TaxCalc.TaxCalculator.Contracts;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddRazorPages();
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddDbContext<TaxCalcDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("TaxCalcDbConnectionString")));
builder.Services.AddAutoMapper(config => config.AddProfile(typeof(TaxCalc.Core.Mapping.CoreProfile)));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICalculationService, CalculationService>();
builder.Services.AddScoped<ICalculatorService, CalculatorService>();
builder.Services.AddScoped<ITaxCalculatorFactory, TaxCalculatorFactory>();
builder.Services.AddScoped<TaxTableConverter>();

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(hostingContext.Configuration)
    .Enrich.FromLogContext());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();