USE TaxCalc;
GO

CREATE TABLE TaxCalculationType
(
    Id INT NOT NULL PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
)
GO

CREATE TABLE PostalCode
(
    Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [Code] VARCHAR(10) NOT NULL,
    TaxCalculationTypeId INT NOT NULL,
    CONSTRAINT FK_PostalCode_TaxCalculationType FOREIGN KEY (TaxCalculationTypeId) REFERENCES TaxCalculationType(Id)
)
GO

CREATE TABLE Tax
(
    Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [From] DECIMAL(18,2) NOT NULL,
    [To] DECIMAL(18,2) NOT NULL,
    Rate DECIMAL(18,3) NOT NULL,
    [Value] DECIMAL(18,2) NOT NULL,
    TaxCalculationTypeId INT NOT NULL
    CONSTRAINT FK_Tax_TaxCalculationType FOREIGN KEY (TaxCalculationTypeId) REFERENCES TaxCalculationType(Id)
)
GO

CREATE TABLE TaxCalculation
(
    Id BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    PostalCodeId INT NOT NULL,
    AnnualIncome DECIMAL(18,2) NOT NULL,
    Result DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_TaxCalculation_PostalCode FOREIGN KEY (PostalCodeId) REFERENCES PostalCode(Id)
)   
GO