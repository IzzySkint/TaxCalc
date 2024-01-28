USE TaxCalc;
GO

CREATE USER TaxCalcApp FOR LOGIN TaxCalcApp;
GO

EXEC sp_addrolemember 'db_datareader', 'TaxCalcApp';
GO

EXEC sp_addrolemember 'db_datawriter', 'TaxCalcApp';
GO