USE master;
GO
IF DB_ID (N'TaxCalc') IS NOT NULL
DROP DATABASE TaxCalc;
GO
CREATE DATABASE TaxCalc;
GO